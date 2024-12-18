using MediatR;
using ScanDetector.Core.Bases;
using ScanDetector.Core.Features.ScannerResult.Queries.Results;

namespace ScanDetector.Core.Features.UsersRelative.Queries.Models
{
    public class GetUsersRelativeQuery:IRequest<BaseResponse<List<GetScannerResponse>>>
    {
        public Guid FilterByUserId { get; set; }
    }
}
