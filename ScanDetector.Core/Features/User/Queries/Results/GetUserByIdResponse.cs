


using ScanDetector.Data.Dtos;

namespace ScanDetector.Core.Features.User.Queries.Results
{
    public class GetUserByIdResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? PictureUrl { get; set; }
        public string Role { get; set; }
        public Guid RoleId { get; set; }
        public List<UserPermissionDto> Permissions { get; set; }
    }
}
