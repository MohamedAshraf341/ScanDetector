using ScanDetector.Data.Entities.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ScanDetector.Infrastructure.Configurations.Authentication
{
    public class AppSettingConfiguration : IEntityTypeConfiguration<AppSetting>
    {
        public void Configure(EntityTypeBuilder<AppSetting> builder)
        {
            // Configure the primary key
            builder.HasKey(appSetting => appSetting.Id);

            // Configure the relationship between AppSetting and User with cascade delete
            builder
                .HasOne(appSetting => appSetting.User) // AppSetting has one User
                .WithMany(user => user.AppSettings) // User has many AppSettings
                .HasForeignKey(appSetting => appSetting.UserId) // Foreign key is UserId in AppSetting
                .OnDelete(DeleteBehavior.Cascade); // Configure cascade delete behavior
        }
    }
}
