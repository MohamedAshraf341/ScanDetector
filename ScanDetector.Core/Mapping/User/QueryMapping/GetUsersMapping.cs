using ScanDetector.Core.Features.User.Queries.Results;

namespace ScanDetector.Core.Mapping.User
{
    public partial class UserProfile
    {
        public void GetUsersMapping()
        {
            CreateMap<Data.Entities.Authentication.User, GetUsersResponse>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.GetLocalized()));
        }
    }
}
