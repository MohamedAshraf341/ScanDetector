using ScanDetector.Data.Helpers;

namespace ScanDetector.Data.Entities.Authentication
{
    public class ModulePermission: LocalizableEntity
    {
        public Guid Id { get; set; }
        public ICollection<Permission> Permissions { get; set; }
    }
}
