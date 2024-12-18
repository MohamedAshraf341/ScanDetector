using ScanDetector.Data.Entities.Authentication;
using ScanDetector.Data.Enums;

namespace ScanDetector.Data.Helpers
{
    public static class AppSettingHelper
    {
        /// <summary>
        /// Converts an AppSetting to a UserSettings object based on the ValueType.
        /// </summary>
        /// <param name="appSetting">The AppSetting object to convert.</param>
        /// <returns>A UserSettings object with the value converted to the appropriate type.</returns>
        public static Setting ToUserSettings(this AppSetting appSetting)
        {
            // Determine the setting type based on the ValueType
            if (!Enum.TryParse(appSetting.ValueType, out SettingType settingType))
            {
                throw new InvalidOperationException($"Invalid ValueType: {appSetting.ValueType}");
            }

            // Parse the value according to the setting type
            object? parsedValue = appSetting.Value != null ? ParseValue(appSetting.Value, settingType) : null;

            // Create and return a new UserSettings object with the parsed value
            return new Setting
            {
                Id = appSetting.Id,
                Name = appSetting.Name,
                Value = parsedValue,
                ValueType = appSetting.ValueType,
                UserId = appSetting.UserId
            };
        }

        /// <summary>
        /// Converts a list of AppSetting objects to a list of UserSettings objects based on the ValueType.
        /// </summary>
        /// <param name="appSettings">The list of AppSetting objects to convert.</param>
        /// <returns>A list of UserSettings objects with the values converted to the appropriate types.</returns>
        public static List<Setting> ToUserSettingsList(this IEnumerable<AppSetting> appSettings)
        {
            return appSettings.Select(appSetting => appSetting.ToUserSettings()).ToList();
        }

        /// <summary>
        /// Parses a string value to its appropriate type based on the SettingType.
        /// </summary>
        /// <param name="value">The value as a string.</param>
        /// <param name="settingType">The SettingType to convert to.</param>
        /// <returns>An object of the correct type.</returns>
        private static object? ParseValue(string value, SettingType settingType)
        {
            return settingType switch
            {
                SettingType.Int => int.TryParse(value, out var intValue) ? intValue : null,
                SettingType.Decimal => decimal.TryParse(value, out var decimalValue) ? decimalValue : null,
                SettingType.Bool => bool.TryParse(value, out var boolValue) ? boolValue : null,
                SettingType.DateTime => DateTime.TryParse(value, out var dateTimeValue) ? dateTimeValue : null,
                SettingType.String => value,
                SettingType.Guid => Guid.TryParse(value, out var guidValue) ? guidValue : null,
                _ => throw new ArgumentOutOfRangeException(nameof(settingType), settingType, null)
            };
        }
    }
}
