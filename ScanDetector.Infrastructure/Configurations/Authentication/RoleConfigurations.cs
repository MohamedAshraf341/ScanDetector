using ScanDetector.Data.Entities.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace ScanDetector.Infrastructure.Configurations.Authentication
{
    public class RoleConfigurations : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);

            // Correct the foreign key mapping to use 'RoleId' instead of 'Role'
            builder.HasMany(x => x.Users)
                   .WithOne(x => x.Role)
                   .HasForeignKey(x => x.RoleId) // Use RoleId instead of Role
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
