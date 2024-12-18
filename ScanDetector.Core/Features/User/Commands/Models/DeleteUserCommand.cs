using ScanDetector.Core.Bases;
using MediatR;

namespace ScanDetector.Core.Features.User.Commands.Models
{
    public class DeleteUserCommand:IRequest<BaseResponse<string>>
    {
        public Guid Id { get; set; }
    }
}
