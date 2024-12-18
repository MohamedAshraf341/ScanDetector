using AutoMapper;

namespace ScanDetector.Core.Mapping.UsersRelative
{
    public partial class UsersRelativeProfile: Profile
    {
        public UsersRelativeProfile() 
        {
            AddUserRelative();
            GetUsersRelativeMapping();
        }
    }
}
