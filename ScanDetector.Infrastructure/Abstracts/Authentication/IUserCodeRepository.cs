using ScanDetector.Data.Entities.Authentication;
using ScanDetector.Infrastructure.Abstracts.Base;

namespace ScanDetector.Infrastructure.Abstracts.Authentication
{
    public interface IUserCodeRepository : IBaseRepository<UserCode>
    {
        Task<UserCode> GetByEmailAndCode(string email, string code);

    }
}
