

using ScanDetector.Core.Bases;
using ScanDetector.Core.Features.User.Queries.Results;
using MediatR;

namespace ScanDetector.Core.Features.User.Queries.Models
{
    public class GetUsersQuery:IRequest<BaseResponse<IEnumerable<GetUsersResponse>>>
    {
    }
}
