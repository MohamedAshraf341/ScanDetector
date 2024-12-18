using ScanDetector.Data.Entities.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ScanDetector.Infrastructure.Configurations.Authentication
{
    public class UserCodeConfiguration : IEntityTypeConfiguration<UserCode>
    {
        public void Configure(EntityTypeBuilder<UserCode> builder)
        {
            // Configure the primary key
            builder.HasKey(userCode => userCode.Id);

            // Configure the relationship between UserCode and User with cascade delete
            builder
                .HasOne(userCode => userCode.User) // UserCode has one User
                .WithMany(user => user.UserCodes) // User has many UserCodes
                .HasForeignKey(userCode => userCode.UserId) // Foreign key is UserId in UserCode
                .OnDelete(DeleteBehavior.Cascade); // Configure cascade delete behavior

            // Configure property constraints and column types as needed
            builder.Property(userCode => userCode.Code)
                   .IsRequired()
                   .HasMaxLength(100); // Example constraint for the Code property

            builder.Property(userCode => userCode.ExpiresOn)
                   .IsRequired(); // ExpiresOn is a required property
        }
    }
}
