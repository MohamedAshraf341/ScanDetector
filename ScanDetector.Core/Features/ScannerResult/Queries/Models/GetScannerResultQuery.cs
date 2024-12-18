using MediatR;
using ScanDetector.Core.Bases;
using ScanDetector.Core.Features.ScannerResult.Queries.Results;

namespace ScanDetector.Core.Features.ScannerResult.Queries.Models
{
    public class GetScannerResultQuery:IRequest<BaseResponse<List<GetScannerResponse>>>
    {
        public Guid? FilterByUserId { get; set; }
        public string? FilterByCameraId { get; set; }
    }
}
