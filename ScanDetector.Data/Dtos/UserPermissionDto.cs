namespace ScanDetector.Data.Dtos
{
    public class UserPermissionDto
    {
        public Guid ModuleId { get; set; }
        public string ModuleName { get; set; }
        public List<PermissionDto> Permissions { get; set; }
    }
}
