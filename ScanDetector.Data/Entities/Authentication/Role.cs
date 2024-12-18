using ScanDetector.Data.Helpers;

namespace ScanDetector.Data.Entities.Authentication
{
    public class Role: LocalizableEntity
    {
        public Guid Id { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
