using ScanDetector.Core.Features.User.Queries.Results;
using ScanDetector.Core.Wrappers;
using ScanDetector.Data.Enums;
using MediatR;

namespace ScanDetector.Core.Features.User.Queries.Models
{
    public class GetUserPaginatedListQuery:IRequest<PaginatedResult<GetUsersResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public UserOrderingEnum OrderBy { get; set; }
        public string? Search { get; set; }
    }
}
