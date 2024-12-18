namespace ScanDetector.Data.Entities.Authentication
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string HashPassword { get; set; }
        public string? PictureUrl { get; set; }
        public string? CameraId { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid RoleId { get; set; }
        public Role? Role { get; set; }
        public ICollection<UserPermission> Permissions { get; set; }
        public ICollection<UserCode> UserCodes { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }
        public ICollection<AppSetting> AppSettings { get; set; }
        public ICollection<ScannerResult> ResultDetectors { get; set; }
        public ICollection<UsersRelative> UsersRelatives { get; set; }
        public ICollection<UserLocation> UserLocations { get; set; }

    }
}
