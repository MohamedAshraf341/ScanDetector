using ScanDetector.Core.Bases;
using MediatR;

namespace ScanDetector.Core.Features.User.Commands.Models
{
    public class AddUserCommand:IRequest<BaseResponse<string>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Guid RoleId { get; set; }
    }
}
