using ScanDetector.Core.Features.Role.Queries.Results;

namespace ScanDetector.Core.Mapping.Role
{
    public partial class RoleProfile
    {
        public void GetRolesMapping()
        {
            CreateMap<Data.Entities.Authentication.Role, GetRolesResponse>();
        }
    }
}
