using ScanDetector.Data.Entities.Authentication;

namespace ScanDetector.Data.Entities
{
    public class ScannerResult
    {
        public Guid Id { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? Duration { get; set; }
        public string? Status { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }

    }
}
