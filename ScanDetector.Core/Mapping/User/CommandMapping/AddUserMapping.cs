using ScanDetector.Core.Features.User.Commands.Models;


namespace ScanDetector.Core.Mapping.User
{
    public partial class UserProfile
    {
        public void AddUserMapping()
        {
            CreateMap<AddUserCommand, Data.Entities.Authentication.User>()
                                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.ToLower()))
;
        }
    }
}
