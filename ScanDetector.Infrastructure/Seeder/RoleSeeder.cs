
using ScanDetector.Data.AppMetaData;
using ScanDetector.Infrastructure.Abstracts.Base;

namespace ScanDetector.Infrastructure.Seeder
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(IUnitOfWork unitOfWork)
        {
            var Admin = await unitOfWork.Roles.GetByIdAsync(RoleData.Admin.Id);
            if (Admin == null)
                await unitOfWork.Roles.AddAsync(RoleData.Admin);
            var User = await unitOfWork.Roles.GetByIdAsync(RoleData.User.Id);
            if (User == null)
                await unitOfWork.Roles.AddAsync(RoleData.User);

            unitOfWork.Complete();
        }
    }
}
