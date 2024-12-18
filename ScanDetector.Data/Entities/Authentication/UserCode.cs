namespace ScanDetector.Data.Entities.Authentication
{
    public class UserCode
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public DateTime ExpiresOn { get; set; }
    }
}
