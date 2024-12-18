using ScanDetector.Data.Entities;
using ScanDetector.Data.Entities.Authentication;
using ScanDetector.Infrastructure.Abstracts.Authentication;
using ScanDetector.Infrastructure.Abstracts.Base;
using ScanDetector.Infrastructure.Data;
using ScanDetector.Infrastructure.Repositories.Authentication;
using Serilog;

namespace ScanDetector.Infrastructure.Repositories.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IBaseRepository<Permission> Permission { get; private set; }

        public IBaseRepository<ModulePermission> ModulePermission { get; private set; }

        public IBaseRepository<UserPermission> UserPermission { get; private set; }

        public IBaseRepository<Role> Roles { get; private set; }

        public IUserRepository Users { get; private set; }


        public IRefreshTokenRepository RefreshTokens { get; private set; }

        public IUserCodeRepository UserCodes { get; private set; }

        public IAppSettingRepository AppSetting { get; private set; }

        public IBaseRepository<ScannerResult> ScannerResult { get; private set; }

        public IBaseRepository<UserLocation> UserLocation { get; private set; }

        public IBaseRepository<UsersRelative> UsersRelative { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Roles = new BaseRepository<Role>(_context);
            RefreshTokens = new RefreshTokenRepository(_context);
            UserCodes = new UserCodeRepository(_context);
            AppSetting = new AppSettingRepository(_context);
            Permission=new BaseRepository<Permission>(_context);
            UserPermission=new BaseRepository<UserPermission>(_context);
            ModulePermission=new BaseRepository<ModulePermission>(_context);
            ScannerResult = new BaseRepository<ScannerResult>(_context);
            UserLocation=new BaseRepository<UserLocation>(_context);
            UsersRelative=new BaseRepository<UsersRelative>(_context);
           
        }

        public int Complete()
        {
            try
            {
                var num = _context.SaveChanges();
                return num;
            } catch(Exception ex)
            {
                Log.Error($"UnHandle Exception : : {ex}");
                return 0;
            }
        }

        public void Dispose() { _context.Dispose(); }
    }
}
