using ScanDetector.Data.Entities.Authentication;

namespace ScanDetector.Data.Entities
{
    public class UsersRelative
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
