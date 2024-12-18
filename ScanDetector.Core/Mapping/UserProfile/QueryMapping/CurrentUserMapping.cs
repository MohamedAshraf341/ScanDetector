using ScanDetector.Core.Features.UserProfile.Queries.Results;

namespace ScanDetector.Core.Mapping.UserProfile
{
    public partial class UserProfileProfile
    {
        public void CurrentUserMapping()
        {
            CreateMap<Data.Entities.Authentication.User, CurrentUserResponse>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.GetLocalized()));

        }
    }
}
