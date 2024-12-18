using ScanDetector.Data.Dtos;

namespace ScanDetector.Core.Mapping.UsersRelative
{
    public partial class UsersRelativeProfile
    {
        public void AddUserRelative()
        {
            CreateMap<AddUsersRelativeDto, Data.Entities.UsersRelative>();
        }
    }
}
