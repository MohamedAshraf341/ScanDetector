
using ScanDetector.Data.AppMetaData;
using ScanDetector.Infrastructure.Abstracts.Base;

namespace ScanDetector.Infrastructure.Seeder
{
    public static class PermissionSeeder
    {
        public static async Task SeedModuleAsync(IUnitOfWork unitOfWork)
        {
           
            var items = PermissionData.allModulePermission;
            foreach(var item in items)
            {
                var module = await unitOfWork.ModulePermission.GetByIdAsync(item.Id);
                if (module == null)
                    await unitOfWork.ModulePermission.AddAsync(item);
            }           
            unitOfWork.Complete();
        }
        public static async Task SeedPermissionAsync(IUnitOfWork unitOfWork)
        {
            var items = PermissionData.allPermission;
            foreach (var item in items)
            {
                var permission = await unitOfWork.Permission.GetByIdAsync(item.Id);
                if (permission == null)
                    await unitOfWork.Permission.AddAsync(item);
            }
            unitOfWork.Complete();
        }
    }
}
