using ScanDetector.Data.Entities.Authentication;
using ScanDetector.Data.Enums;
namespace ScanDetector.Data.Helpers
{
    public static class SettingTypeHelper
    {
        public static string GetFullTypeName(SettingType settingType)
        {
            return settingType switch
            {
                SettingType.Int => typeof(int).FullName,
                SettingType.Decimal => typeof(decimal).FullName,
                SettingType.Bool => typeof(bool).FullName,
                SettingType.DateTime => typeof(DateTime).FullName,
                SettingType.String => typeof(string).FullName,
                SettingType.Guid => typeof(Guid).FullName,
                _ => throw new ArgumentOutOfRangeException(nameof(settingType), settingType, null)
            };
        }
    }

}
