using ScanDetector.Data.Entities.Authentication;
using ScanDetector.Infrastructure.Abstracts.Base;
using ScanDetector.Service.Abstracts;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ScanDetector.Service.Implementations
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<User> GetUserAsync()
        {
            var userId = GetUserId();
            var user=await _unitOfWork.Users.GetByID(userId);
            if (user == null)
            { throw new UnauthorizedAccessException(); }
            return user;
        }

        public Guid GetUserId()
        {
            var claims = _httpContextAccessor.HttpContext?.User?.Claims;
            // Use ClaimTypes.NameIdentifier instead of JwtRegisteredClaimNames.NameId
            var userIdClaim = claims?.SingleOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out Guid userId))
            {
                throw new UnauthorizedAccessException();
            }

            return userId;
        }

        public string GetUserRole()
        {
            var claims = _httpContextAccessor.HttpContext?.User?.Claims;
            var userRoleClaim = claims?.SingleOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(userRoleClaim))
            {
                throw new UnauthorizedAccessException();
            }

            return userRoleClaim;
        }

    }
}
