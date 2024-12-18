using ScanDetector.Core.Bases;
using MediatR;

namespace ScanDetector.Core.Features.UserProfile.Commands.Models
{
    public class LogoutCommand : IRequest<BaseResponse<string>>
    {
        public string RefreshToken { get; set; }
    }
}
