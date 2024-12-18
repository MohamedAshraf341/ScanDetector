using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScanDetector.Data.Entities;

namespace ScanDetector.Infrastructure.Configurations
{
    public class ScannerResultConfiguration : IEntityTypeConfiguration<ScannerResult>
    {
        public void Configure(EntityTypeBuilder<ScannerResult> builder)
        {
            builder.HasOne(x => x.User)
                   .WithMany(x => x.ResultDetectors)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
