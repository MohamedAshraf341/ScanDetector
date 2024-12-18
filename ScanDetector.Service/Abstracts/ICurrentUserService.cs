using ScanDetector.Data.Entities.Authentication;

namespace ScanDetector.Service.Abstracts
{
    public interface ICurrentUserService
    {
        public Task<User> GetUserAsync();
        public Guid GetUserId();
        public string GetUserRole();

    }
}
