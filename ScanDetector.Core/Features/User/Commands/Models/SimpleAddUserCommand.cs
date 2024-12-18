using ScanDetector.Core.Bases;
using MediatR;

namespace ScanDetector.Core.Features.User.Commands.Models
{
    public class SimpleAddUserCommand:IRequest<BaseResponse<string>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid RoleId { get; set; }
    }
}
