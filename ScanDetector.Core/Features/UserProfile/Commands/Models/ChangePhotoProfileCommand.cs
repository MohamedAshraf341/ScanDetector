using ScanDetector.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ScanDetector.Core.Features.UserProfile.Commands.Models
{
    public class ChangePhotoProfileCommand:IRequest<BaseResponse<string>>
    {
        public bool? IsRemove { get; set; }
        public IFormFile? File { get; set; }
    }
}
