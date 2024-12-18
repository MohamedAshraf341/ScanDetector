using AutoMapper;


namespace ScanDetector.Core.Mapping.AppSetting
{
    public partial class AppSettingProfile: Profile
    {
        public AppSettingProfile() 
        {
            GetSettingsMapping();
        }
    }
}
