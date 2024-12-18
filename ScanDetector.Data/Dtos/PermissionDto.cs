namespace ScanDetector.Data.Dtos
{
    public class PermissionDto
    {
        public Guid Id { get; set; }
        public Guid PermissionId { get; set; }
        public string PermissionName { get; set; }
        public bool IsSelected { get; set; }
    }
}
