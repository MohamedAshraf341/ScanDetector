using ScanDetector.Data.Entities.Authentication;
using ScanDetector.Infrastructure.Abstracts.Base;

namespace ScanDetector.Infrastructure.Abstracts.Authentication
{
    public interface IRefreshTokenRepository : IBaseRepository<RefreshToken>
    {
        Task<RefreshToken> GetByRefreshToken(string refreshToken);

    }
}
