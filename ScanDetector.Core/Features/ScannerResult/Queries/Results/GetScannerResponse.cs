

namespace ScanDetector.Core.Features.ScannerResult.Queries.Results
{
    public class GetScannerResponse
    {
        public Guid Id { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? Duration { get; set; }
        public string? Status { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
    }
}
