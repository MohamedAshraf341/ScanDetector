using ScanDetector.Data.Entities;
using ScanDetector.Data.Entities.Authentication;
using ScanDetector.Infrastructure.Abstracts.Authentication;

namespace ScanDetector.Infrastructure.Abstracts.Base
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<ScannerResult> ScannerResult { get; }
        IBaseRepository<UserLocation> UserLocation { get; }
        IBaseRepository<UsersRelative> UsersRelative { get; }

        IBaseRepository<Role> Roles { get; }
        IBaseRepository<Permission> Permission { get; }
        IBaseRepository<ModulePermission> ModulePermission { get; }
        IBaseRepository<UserPermission> UserPermission { get; }
        IUserRepository Users { get; }
        IUserCodeRepository UserCodes { get; }
        IRefreshTokenRepository RefreshTokens { get; }
        IAppSettingRepository AppSetting { get; }

        int Complete();
    }
}
