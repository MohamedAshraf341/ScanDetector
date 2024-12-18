using MediatR;
using ScanDetector.Core.Bases;
using ScanDetector.Data.Dtos;

namespace ScanDetector.Core.Features.UsersRelative.Commands.Models
{
    public class AddUsersRelativeCommand:IRequest<BaseResponse<string>>
    {
        public List<AddUsersRelativeDto> Data { get; set; }
    }
}
