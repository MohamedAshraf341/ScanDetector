using ScanDetector.Core.Features.User.Queries.Results;
using ScanDetector.Data.Dtos;
using ScanDetector.Data.Entities.Authentication;

namespace ScanDetector.Core.Mapping.User
{
    public partial class UserProfile
    {
        public void GetUserByIdMapping()
        {
            CreateMap<Data.Entities.Authentication.User, GetUserByIdResponse>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role != null ? src.Role.GetLocalized() : string.Empty))
            .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src =>
                src.Permissions
                    .GroupBy(up => new { up.Permission.ModulePermission.Id, V = up.Permission.ModulePermission.GetLocalized() })
                    .Select(group => new UserPermissionDto
                    {
                        ModuleId = group.Key.Id,
                        ModuleName = group.Key.V,
                        Permissions = group.Select(up => new PermissionDto
                        {
                            Id = up.Id,
                            PermissionId = up.Permission.Id,
                            PermissionName = up.Permission.GetLocalized(),
                            IsSelected = up.IsSelected
                        }).ToList()
                    }).ToList()
            ));

            CreateMap<UserPermission, PermissionDto>()
                .ForMember(dest => dest.PermissionId, opt => opt.MapFrom(src => src.Permission.Id))
                .ForMember(dest => dest.PermissionName, opt => opt.MapFrom(src => src.Permission.GetLocalized()))
                .ForMember(dest => dest.IsSelected, opt => opt.MapFrom(src => src.IsSelected));


        }
    }
}
