using ScanDetector.Data.Entities.Authentication;
using ScanDetector.Infrastructure.Abstracts.Authentication;
using ScanDetector.Infrastructure.Data;
using ScanDetector.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace ScanDetector.Infrastructure.Repositories.Authentication
{
    public class AppSettingRepository : BaseRepository<AppSetting>, IAppSettingRepository
    {
        public AppSettingRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task AddRange(List<AppSetting> items)
        {
            await _context.AppSettings.AddRangeAsync(items);
        }

        public void DeleteRange(IEnumerable<AppSetting> items)
        {
            _context.AppSettings.RemoveRange(items);
        }

        public async Task<List<AppSetting>> GetAllByIds(List<Guid> ids)
        {
            var items = await _context.AppSettings.Where(x => ids.Contains(x.Id)).ToListAsync();
            return items;
        }

        public async Task<IEnumerable<AppSetting>> GetAllByUserId(Guid userId)
        {
            var items = await _context.AppSettings.Where(x => x.UserId == userId).ToListAsync();
            return items;
        }

        public async Task<AppSetting> GetByUserIdAndName(Guid userId, string name)
        {
            var item = await _context.AppSettings.Where(x => x.UserId == userId && x.Name == name).FirstOrDefaultAsync();
            return item;
        }

        public async Task<IEnumerable<AppSetting>> GetSmtpSettingsByUserId(Guid userId)
        {
            var items = await _context.AppSettings.Where(x => x.UserId == userId && x.Name.Contains("Smtp")).ToListAsync();
            return items;
        }
    }
}
