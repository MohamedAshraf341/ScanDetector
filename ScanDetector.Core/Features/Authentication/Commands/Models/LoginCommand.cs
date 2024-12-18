using ScanDetector.Core.Bases;
using ScanDetector.Core.Features.Authentication.Commands.Results;
using MediatR;

namespace ScanDetector.Core.Features.Authentication.Commands.Models
{
    public class LoginCommand:IRequest<BaseResponse<TokenResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
