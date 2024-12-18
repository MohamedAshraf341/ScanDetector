using ScanDetector.Data.Entities.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ScanDetector.Infrastructure.Configurations.Authentication
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            // Configure the primary key
            builder.HasKey(refreshToken => refreshToken.Id);

            // Configure the relationship between RefreshToken and User with cascade delete
            builder
                .HasOne(refreshToken => refreshToken.User) // RefreshToken has one User
                .WithMany(user => user.RefreshTokens) // User has many RefreshTokens
                .HasForeignKey(refreshToken => refreshToken.UserId) // Foreign key is UserId in RefreshToken
                .OnDelete(DeleteBehavior.Cascade); // Configure cascade delete behavior

            // Configure property constraints and column types as needed
            builder.Property(refreshToken => refreshToken.Token)
                   .IsRequired()
                   .HasMaxLength(500); // Example constraint for the Token property

            builder.Property(refreshToken => refreshToken.CreatedOn)
                   .IsRequired();

            builder.Property(refreshToken => refreshToken.ExpiresOn)
                   .IsRequired();

            builder.Property(refreshToken => refreshToken.RevokedOn)
                   .IsRequired(false); // RevokedOn can be null since it represents an optional property
        }
    }
}
