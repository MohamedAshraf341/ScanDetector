using ScanDetector.Data.AppMetaData;
using ScanDetector.Data.Entities.Authentication;
using ScanDetector.Infrastructure.Abstracts.Base;
using System.Security;

namespace ScanDetector.Infrastructure.Seeder
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(IUnitOfWork unitOfWork)
        {
            var Admin = await unitOfWork.Users.GetByIdAsync(UserData.Admin.Id);
            if (Admin == null)
                await unitOfWork.Users.AddAsync(UserData.Admin);
            var User = await unitOfWork.Users.GetByIdAsync(UserData.User.Id);
            if (User == null)
            {
                var user = UserData.User;
                user.Permissions= PermissionData.allPermission.Select(x => new UserPermission
                {
                    IsSelected = false,
                    PermissionId = x.Id,
                    UserId = UserData.User.Id
                }).ToList();
                await unitOfWork.Users.AddAsync(user);
            }

            unitOfWork.Complete();
        }
    }
}
