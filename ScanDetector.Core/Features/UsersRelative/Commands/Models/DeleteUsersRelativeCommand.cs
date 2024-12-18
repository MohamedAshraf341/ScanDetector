

using MediatR;
using ScanDetector.Core.Bases;

namespace ScanDetector.Core.Features.UsersRelative.Commands.Models
{
    public class DeleteUsersRelativeCommand : IRequest<BaseResponse<string>>
    {
        public Guid Id { get; set; }
    }
}
