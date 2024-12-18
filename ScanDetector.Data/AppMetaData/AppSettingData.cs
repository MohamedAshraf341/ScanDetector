using ScanDetector.Data.Entities.Authentication;
using ScanDetector.Data.Enums;

namespace ScanDetector.Data.AppMetaData
{
    public class AppSettingData
    {
        public static readonly List<AppSetting> all = new();

        public static readonly AppSetting SmtpHost = New("SmtpHost", SettingType.String.ToString());
        public static readonly AppSetting SmtpPort = New("SmtpPort", SettingType.Int.ToString());
        public static readonly AppSetting SmtpUseSSL = New("SmtpUseSSL", SettingType.Bool.ToString());
        public static readonly AppSetting SmtpDisplayName = New("SmtpDisplayName", SettingType.String.ToString());
        public static readonly AppSetting SmtpEmail = New("SmtpEmail", SettingType.String.ToString());
        public static readonly AppSetting SmtpPassword = New("SmtpPassword", SettingType.String.ToString());
        public static readonly AppSetting Language = new AppSetting() { Name = "Language", ValueType = SettingType.String.ToString() };

        protected static AppSetting New(string name, string type)
        {
            var r = new AppSetting { Name = name, ValueType = type };
            all.Add(r);
            return r;
        }
    }
}
