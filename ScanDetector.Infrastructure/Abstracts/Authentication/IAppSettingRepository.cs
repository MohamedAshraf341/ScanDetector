using ScanDetector.Data.Entities.Authentication;
using ScanDetector.Infrastructure.Abstracts.Base;

namespace ScanDetector.Infrastructure.Abstracts.Authentication
{
    public interface IAppSettingRepository : IBaseRepository<AppSetting>
    {
        Task<AppSetting> GetByUserIdAndName(Guid userId, string name);

        Task<IEnumerable<AppSetting>> GetAllByUserId(Guid userId);
        Task<List<AppSetting>> GetAllByIds(List<Guid> ids);

        Task<IEnumerable<AppSetting>> GetSmtpSettingsByUserId(Guid userId);

        Task AddRange(List<AppSetting> items);
        void DeleteRange(IEnumerable<AppSetting> items);


    }
}
