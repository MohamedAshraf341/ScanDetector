

using ScanDetector.Core.Bases;
using MediatR;

namespace ScanDetector.Core.Features.User.Commands.Models
{
    public class AssignRoleToUserCommand:IRequest<BaseResponse<string>>
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
