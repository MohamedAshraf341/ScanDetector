

using AutoMapper;
using ScanDetector.Core.Features.Authentication.Commands.Results;
using ScanDetector.Data.Dtos;

namespace ScanDetector.Core.Mapping.Authentication
{
    public partial class AuthenticationProfile : Profile
    {
        public void TokenMapping()
        {
            CreateMap<Data.Entities.Authentication.User, TokenResponse>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role != null ? src.Role.NameEn : string.Empty))
            .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src =>
                src.Permissions
                    .GroupBy(up => new { up.Permission.ModulePermission.Id, up.Permission.ModulePermission.NameEn })
                    .Select(group => new UserPermissionTokenDto
                    {
                        ModuleName = group.Key.NameEn,
                        Permissions = group.Select(up => new PermissionTokenDto
                        {
                            PermissionName = up.Permission.NameEn,
                            IsSelected = up.IsSelected
                        }).ToList()
                    }).ToList()
            ));

        }
    }
}
