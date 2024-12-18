using ScanDetector.Core.Bases;
using MediatR;

namespace ScanDetector.Core.Features.UserProfile.Commands.Models
{
    public class ChangePasswordCommand:IRequest<BaseResponse<string>>
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
