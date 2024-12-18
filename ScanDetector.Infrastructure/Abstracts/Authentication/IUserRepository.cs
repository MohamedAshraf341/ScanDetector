using ScanDetector.Data.Entities.Authentication;
using ScanDetector.Data.Enums;
using ScanDetector.Infrastructure.Abstracts.Base;

namespace ScanDetector.Infrastructure.Abstracts.Authentication
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByID(Guid id);

        Task<User> GetByEmail(string email);
        Task<User> GetByEmailNotId(string email,Guid id);

        Task<User> GetByRefreshToken(string refreshToken);
        Task<IEnumerable<User>> GetAllIncludeRole(Guid Id);
        IQueryable<User> FilterStudentPaginatedQueryable(Guid? id, UserOrderingEnum userEnum, string search);

    }
}
