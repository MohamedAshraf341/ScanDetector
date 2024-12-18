using ScanDetector.Core.Bases;
using ScanDetector.Core.Features.Authentication.Commands.Results;
using MediatR;

namespace ScanDetector.Core.Features.Authentication.Commands.Models
{
    public class ResetPasswordCommand:IRequest<BaseResponse<TokenResponse>>
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}
