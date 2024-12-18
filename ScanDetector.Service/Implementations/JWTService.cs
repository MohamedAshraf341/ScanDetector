using ScanDetector.Data.Entities.Authentication;
using ScanDetector.Data.Helpers;
using ScanDetector.Service.Abstracts;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ScanDetector.Data.Dtos;


namespace ScanDetector.Service.Implementations
{
    public class JWTService: IJWTService
    {
        private readonly JwtSettings _jwt;

        public JWTService(JwtSettings jwt)
        {
            _jwt = jwt;
        }
        public JwtSecurityToken CreateJwtToken(User user, List<UserPermissionTokenDto> permissions)
        {
            // Flatten permissions to a list of permission names
            var flattenedPermissions = permissions
                .SelectMany(module => module.Permissions)
                .Select(permission => permission.PermissionName)
                .Distinct() // Remove duplicates if any
                .ToList();

            // Serialize the flattened permissions list
            var permissionsJson = System.Text.Json.JsonSerializer.Serialize(flattenedPermissions);

            // Define claims
            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Name, $"{user.FirstName} {user.LastName}"),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
        new Claim(ClaimTypes.Role, user.Role.NameEn),
        new Claim("Permissions", permissionsJson) // Add flattened permissions as a claim
    };

            // Create signing credentials
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            // Generate JWT
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials
            );

            return jwtSecurityToken;
        }

        public RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[32];

            using var generator = new RNGCryptoServiceProvider();

            generator.GetBytes(randomNumber);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpiresOn = DateTime.UtcNow.AddDays(60),
                CreatedOn = DateTime.UtcNow
            };
        }
    }
}
