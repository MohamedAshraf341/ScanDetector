

using ScanDetector.Core.Bases;
using ScanDetector.Core.Features.User.Queries.Results;
using MediatR;

namespace ScanDetector.Core.Features.User.Queries.Models
{
    public class GetUserByIdQuery:IRequest<BaseResponse<GetUserByIdResponse>>
    {
        public Guid Id { get; set; }
    }
}
