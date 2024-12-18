using AutoMapper;

namespace ScanDetector.Core.Mapping.UserProfile
{
    public partial class UserProfileProfile: Profile
    {
        public UserProfileProfile() 
        {
            CurrentUserMapping();
            UpdateProfileMapping();
        }
    }
}
