using ScanDetector.Data.Entities.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ScanDetector.Data.Entities;

namespace ScanDetector.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ScannerResult> ScannerResult { get; set; }
        public DbSet<UserLocation> UserLocation { get; set; }
        public DbSet<UsersRelative> UsersRelative { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<UserCode> UserCodes { get; set; }
        public DbSet<AppSetting> AppSettings { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<ModulePermission> ModulePermissions { get; set; }
        public DbSet<Permission> UserPermissions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
