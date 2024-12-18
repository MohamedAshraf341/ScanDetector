

using MediatR;
using ScanDetector.Core.Bases;

namespace ScanDetector.Core.Features.ScannerResult.Commands.Models
{
    public class AddScannerResultCommand:IRequest<BaseResponse<string>>
    {
        public string CameraId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? Duration { get; set; }
        public string? Status { get; set; }
    }
}
