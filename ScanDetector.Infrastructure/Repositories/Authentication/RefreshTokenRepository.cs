using ScanDetector.Data.Entities.Authentication;
using ScanDetector.Infrastructure.Abstracts.Authentication;
using ScanDetector.Infrastructure.Data;
using ScanDetector.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace ScanDetector.Infrastructure.Repositories.Authentication
{
    public class RefreshTokenRepository : BaseRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(ApplicationDbContext context) : base(context)
        {
        }


        public async Task<RefreshToken> GetByRefreshToken(string refreshToken)
        {
            var RefreshToken = await _context.RefreshTokens.Where(x => x.Token == refreshToken).FirstOrDefaultAsync();
            return RefreshToken;
        }
    }
}
