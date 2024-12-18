namespace ScanDetector.Core.Features.UserProfile.Queries.Results
{
    public class CurrentUserResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PictureUrl { get; set; }
        public string CameraId { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }


    }
}
