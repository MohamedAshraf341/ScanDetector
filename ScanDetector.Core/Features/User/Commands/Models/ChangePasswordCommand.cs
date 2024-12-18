using MediatR;
using ScanDetector.Core.Bases;

namespace ScanDetector.Core.Features.User.Commands.Models
{
    public class ChangePasswordCommand : IRequest<BaseResponse<string>>
    {
        public Guid Id { get; set; }
        public string Password { get; set; }
    }
}
