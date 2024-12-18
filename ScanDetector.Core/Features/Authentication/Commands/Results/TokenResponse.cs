
using ScanDetector.Data.Dtos;

namespace ScanDetector.Core.Features.Authentication.Commands.Results
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public DateTime TokenExpiresOn { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
        public string Role { get; set; }
        public List<UserPermissionTokenDto> Permissions { get; set;}
    }
}
