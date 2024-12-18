using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScanDetector.Data.Entities;

namespace ScanDetector.Infrastructure.Configurations
{

    public class UsersRelativeConfiguration : IEntityTypeConfiguration<UsersRelative>
    {
        public void Configure(EntityTypeBuilder<UsersRelative> builder)
        {
            builder.HasOne(x => x.User)
                   .WithMany(x => x.UsersRelatives)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
