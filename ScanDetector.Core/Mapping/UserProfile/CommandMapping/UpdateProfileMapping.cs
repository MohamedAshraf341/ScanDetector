using ScanDetector.Core.Features.UserProfile.Commands.Models;

namespace ScanDetector.Core.Mapping.UserProfile
{
    public partial class UserProfileProfile
    {
        public void UpdateProfileMapping()
        {
            CreateMap<UpdateProfileCommand, Data.Entities.Authentication.User>();
        }
    }
}
