using ScanDetector.Data.Entities.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ScanDetector.Infrastructure.Configurations.Authentication
{
    public class PermissionConfigurations : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.ModulePermission)
                   .WithMany(x => x.Permissions)
                   .HasForeignKey(x => x.ModulePermissionId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
