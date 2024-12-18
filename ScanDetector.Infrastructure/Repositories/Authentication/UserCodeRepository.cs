using ScanDetector.Data.Entities.Authentication;
using ScanDetector.Infrastructure.Abstracts.Authentication;
using ScanDetector.Infrastructure.Data;
using ScanDetector.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace ScanDetector.Infrastructure.Repositories.Authentication
{
    public class UserCodeRepository : BaseRepository<UserCode>, IUserCodeRepository
    {
        public UserCodeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<UserCode> GetByEmailAndCode(string email, string code)
        {
            var userCode = await _context.UserCodes.Include(x => x.User)
                .Where(x => x.Code == code && x.User.Email == email).FirstOrDefaultAsync();
            return userCode;
        }

    }
}
