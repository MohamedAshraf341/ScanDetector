using AutoMapper;
using ScanDetector.Core.Features.User.Commands.Models;
using ScanDetector.Data.Entities.Authentication;

namespace ScanDetector.Core.Mapping.User
{
    public partial class UserProfile 
    {

        public void UpdateUserMapping()
        {
            CreateMap<UpdateUserCommand, Data.Entities.Authentication.User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Prevent overwriting the ID                
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.ToLower())) // Normalize email
                .ForMember(dest => dest.PictureUrl, opt => opt.Ignore()) // Avoid overriding PictureUrl
                .ForMember(dest => dest.Role, opt => opt.Ignore()) // Role should not be updated directly
                .ForMember(dest => dest.UserCodes, opt => opt.Ignore()) // Skip updating UserCodes
                .ForMember(dest => dest.RefreshTokens, opt => opt.Ignore()) // Skip updating RefreshTokens
                .ForMember(dest => dest.AppSettings, opt => opt.Ignore()); // Skip updating AppSettings
        }
    }
}
