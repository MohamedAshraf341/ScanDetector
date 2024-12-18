

namespace ScanDetector.Core.Features.UsersRelative.Queries.Results
{
    public class GetUsersRelativeResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
