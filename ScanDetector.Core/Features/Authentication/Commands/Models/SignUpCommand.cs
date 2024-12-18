using ScanDetector.Core.Bases;
using ScanDetector.Core.Features.Authentication.Commands.Results;
using MediatR;

namespace ScanDetector.Core.Features.Authentication.Commands.Models
{
    public class SignUpCommand: IRequest<BaseResponse<TokenResponse>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CameraId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
