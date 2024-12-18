
using ScanDetector.Core.Features.AppSetting.Commands.Results;

namespace ScanDetector.Core.Mapping.AppSetting
{
    public partial class AppSettingProfile
    {
        public void GetSettingsMapping()
        {
            CreateMap<UserSettings,Data.Entities.Authentication.AppSetting>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value.ToString()));
        }
    }
    
}
