using ScanDetector.Core.Features.User.Commands.Models;


namespace ScanDetector.Core.Mapping.User
{
    public partial class UserProfile
    {
        public void SimpleAddUserMapping()
        {
            CreateMap<SimpleAddUserCommand, Data.Entities.Authentication.User>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.ToLower()))
                .ForMember(dest => dest.HashPassword, opt => opt.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.Password)));
        }
    }
}
