using ScanDetector.Core.Bases;
using MediatR;
namespace ScanDetector.Core.Features.Authentication.Commands.Models
{
    public class SendCodeResetPasswordCommand:IRequest<BaseResponse<string>>
    {
        public string Email { get; set; }
    }
}
