using ScanDetector.Data.Entities.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace ScanDetector.Infrastructure.Configurations.Authentication
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName).HasMaxLength(50);
            builder.Property(x => x.LastName).HasMaxLength(50);

            builder.Property(x => x.Email).HasMaxLength(150);

            builder.Property(x => x.HashPassword).HasMaxLength(150);
            builder.Property(x => x.PictureUrl).HasMaxLength(500);

            builder.HasOne(x => x.Role)
                     .WithMany(y => y.Users)
                     .HasForeignKey(x => x.RoleId);

            builder.HasMany(x => x.RefreshTokens)
                      .WithOne(x => x.User)
                      .HasForeignKey(x => x.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
