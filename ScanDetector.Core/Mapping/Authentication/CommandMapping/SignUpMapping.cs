

using AutoMapper;
using ScanDetector.Core.Features.Authentication.Commands.Models;

namespace ScanDetector.Core.Mapping.Authentication
{
    public partial class AuthenticationProfile : Profile
    {
        public void SignUpMapping()
        {
            CreateMap<SignUpCommand, Data.Entities.Authentication.User>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.ToLower()))
                .ForMember(dest => dest.HashPassword, opt => opt.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.Password)));

        }
    }
}
