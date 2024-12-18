using ScanDetector.Data.Entities.Authentication;

namespace ScanDetector.Data.Entities
{
    public class UserLocation
    {
        public Guid Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime CreationDateTime { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
