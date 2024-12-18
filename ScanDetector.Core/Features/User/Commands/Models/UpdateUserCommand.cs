using ScanDetector.Core.Bases;
using MediatR;
using ScanDetector.Data.Dtos;

namespace ScanDetector.Core.Features.User.Commands.Models
{
    public class UpdateUserCommand:IRequest<BaseResponse<string>>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Guid RoleId { get; set; }
        public List<UserPermissionDto> Permissions { get; set; }

    }
}
