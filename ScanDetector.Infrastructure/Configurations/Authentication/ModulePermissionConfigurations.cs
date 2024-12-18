using ScanDetector.Data.Entities.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ScanDetector.Infrastructure.Configurations.Authentication
{
    public class ModulePermissionConfigurations : IEntityTypeConfiguration<ModulePermission>
    {
        public void Configure(EntityTypeBuilder<ModulePermission> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Permissions)
                   .WithOne(x => x.ModulePermission)
                   .HasForeignKey(x => x.ModulePermissionId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
