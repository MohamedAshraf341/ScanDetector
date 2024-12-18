using ScanDetector.Data.AppMetaData;
using ScanDetector.Data.Entities.Authentication;
using ScanDetector.Data.Enums;
using ScanDetector.Infrastructure.Abstracts.Authentication;
using ScanDetector.Infrastructure.Data;
using ScanDetector.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace ScanDetector.Infrastructure.Repositories.Authentication
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IQueryable<User> FilterStudentPaginatedQueryable(Guid? id ,UserOrderingEnum userEnum, string search)
        {
            var query = _context.Users.AsNoTracking().Where(x => x.Id != id && x.RoleId != RoleData.Admin.Id).Include(x => x.Role).AsQueryable();
            if (search != null)
            {
                query = query.Where(x => x.FirstName.Contains(search)
                || x.LastName.Contains(search) || x.Role.NameEn.Contains(search)
                || x.Role.NameAr.Contains(search)
                || x.Email.Contains(search));
            }
            switch (userEnum)
            {
                case UserOrderingEnum.Name:
                    query = query.OrderBy(x => x.FirstName).ThenBy(x => x.LastName);
                    break;
                case UserOrderingEnum.Email:
                    query = query.OrderBy(x => x.Email);
                    break;
                case UserOrderingEnum.RoleId:
                    query = query.OrderBy(x => x.RoleId);
                    break;
            }
            return query;
        }

        public async Task<IEnumerable<User>> GetAllIncludeRole(Guid Id)
        {
            var items = await _context.Users.Where(x => x.Id != Id && x.RoleId != RoleData.Admin.Id).Include(x => x.Role).ToListAsync();
            return items;
        }

        public async Task<User> GetByEmail(string email)
        {
            var user = await _context.Users
                .Include(x => x.Permissions)
                .ThenInclude(x => x.Permission)
                .ThenInclude(x => x.ModulePermission)
                .Include(x => x.Role)
                .Include(x => x.RefreshTokens)
                .FirstOrDefaultAsync(x => x.Email == email);
            return user;

        }
        public async Task<User> GetByEmailNotId(string email, Guid id)
        {
            var user = await _context.Users.Include(x => x.Role).Include(x => x.RefreshTokens).FirstOrDefaultAsync(x => x.Email == email && x.Id != id);
            return user;

        }
        public async Task<User> GetByID(Guid id)
        {
            var item = await _context.Users.Where(x => x.Id == id)
                .Include(x => x.Permissions)
                .ThenInclude(x => x.Permission)
                .ThenInclude(x => x.ModulePermission)
                .Include(x => x.Role).FirstOrDefaultAsync();
            return item;
        }

        public async Task<User> GetByRefreshToken(string refreshToken)
        {
            var user = await _context.Users.Include(x => x.Role).Include(u => u.RefreshTokens).SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == refreshToken));
            return user;
        }
    }
}
