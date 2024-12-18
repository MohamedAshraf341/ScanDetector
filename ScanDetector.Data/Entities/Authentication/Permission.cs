using ScanDetector.Data.Helpers;

namespace ScanDetector.Data.Entities.Authentication
{
    public class Permission: LocalizableEntity
    {
        public Guid Id { get; set; }
        public Guid ModulePermissionId { get; set; }
        public ModulePermission ModulePermission { get; set; }

    }
}
