namespace ScanDetector.Data.Dtos
{
    public class UserPermissionTokenDto
    {
        public string ModuleName { get; set; }
        public List<PermissionTokenDto> Permissions { get; set; }
    }
}
