using ScanDetector.Core.Bases;
using ScanDetector.Core.Features.Role.Queries.Results;
using MediatR;

namespace ScanDetector.Core.Features.Role.Queries.Models
{
    public class GetRolesQuery:IRequest<BaseResponse<List<GetRolesResponse>>>
    {
    }
}
