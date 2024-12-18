namespace ScanDetector.Data.Entities.Authentication
{
    public class AppSetting
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Value { get; set; }
        public string? ValueType { get; set; }
        public Guid? UserId { get; set; }
        public User User { get; set; }
    }
}
