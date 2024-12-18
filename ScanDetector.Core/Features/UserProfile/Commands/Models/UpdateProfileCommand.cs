
using ScanDetector.Core.Bases;
using MediatR;

namespace ScanDetector.Core.Features.UserProfile.Commands.Models
{
    public class UpdateProfileCommand:IRequest<BaseResponse<string>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CameraId { get; set; }
        public string PhoneNumber { get; set; }
    }
}
