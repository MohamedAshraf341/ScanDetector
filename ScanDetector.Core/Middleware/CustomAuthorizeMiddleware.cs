using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Text.Json;

namespace ScanDetector.Core.Middleware
{
    public class CustomAuthorizeMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomAuthorizeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.GetEndpoint();

            if (endpoint != null)
            {
                var customAuthorizeAttribute = endpoint.Metadata.GetMetadata<CustomAuthorizeAttribute>();

                if (customAuthorizeAttribute != null && customAuthorizeAttribute.Permissions != null)
                {
                    HandleAuthorization(context, customAuthorizeAttribute);
                }
            }

            await _next(context);
        }

        private void HandleAuthorization(HttpContext context, CustomAuthorizeAttribute customAuthorizeAttribute)
        {
            if (!IsUserAuthenticated(context))
                throw new UnauthorizedAccessException();

            if (!IsUserAdmin(context))
            {
                var userPermissions = GetUserPermissions(context);

                if (userPermissions == null || !HasRequiredPermissions(userPermissions, customAuthorizeAttribute.Permissions))
                    throw new UnauthorizedAccessException();
            }
        }

        private bool IsUserAuthenticated(HttpContext context)
        {
            return context.User.Identity?.IsAuthenticated ?? false;
        }

        private bool IsUserAdmin(HttpContext context)
        {
            var roleClaim = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            return roleClaim != null && roleClaim.Value == "Admin";
        }

        private string[]? GetUserPermissions(HttpContext context)
        {
            var permissionsClaim = context.User.Claims.FirstOrDefault(c => c.Type == "Permissions");

            if (permissionsClaim == null)
                return null;

            return JsonSerializer.Deserialize<string[]>(permissionsClaim.Value);
        }

        private bool HasRequiredPermissions(string[] userPermissions, string[] requiredPermissions)
        {
            return requiredPermissions.All(permission => userPermissions.Contains(permission));
        }
    }
}
