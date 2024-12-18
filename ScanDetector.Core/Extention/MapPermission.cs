using ScanDetector.Data.Dtos;
using ScanDetector.Data.Entities.Authentication;

namespace ScanDetector.Core.Extention
{
    public class MapPermission
    {
        public static ICollection<UserPermission> UpdateIsSelectedProperty(
      List<UserPermissionDto> sourcePermissions,
      ICollection<UserPermission> destinationPermissions)
        {
            if (sourcePermissions == null || destinationPermissions == null)
                return destinationPermissions ?? new List<UserPermission>();

            foreach (var destPermission in destinationPermissions)
            {
                var matchingSource = sourcePermissions
                    .SelectMany(sp => sp.Permissions)
                    .FirstOrDefault(sp => sp.PermissionId == destPermission.PermissionId);

                if (matchingSource != null)
                    destPermission.IsSelected = matchingSource.IsSelected;
            }

            return destinationPermissions;
        }
    }
}
