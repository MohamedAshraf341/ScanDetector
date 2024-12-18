using ScanDetector.Data.Dtos;
using ScanDetector.Data.Entities.Authentication;
using System.IdentityModel.Tokens.Jwt;

namespace ScanDetector.Service.Abstracts
{
    public interface IJWTService
    {
        JwtSecurityToken CreateJwtToken(User user,List<UserPermissionTokenDto> permissions);
        RefreshToken GenerateRefreshToken();
    }
}
