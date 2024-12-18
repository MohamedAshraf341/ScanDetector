namespace ScanDetector.Data.AppMetaData
{
    public static class Router
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Rule = $"{Root}/{Version}";
        public const string Id = "{id}";
        public static class ReportRouting
        {
            public const string Prefix = $"{Rule}/Report";
            public const string Generate = $"{Prefix}/Generate";

        }
        public static class AppSettingRouting
        {
            public const string Prefix = $"{Rule}/AppSetting";
            public const string SmtpSettingsToCurrentUser = $"{Prefix}/SmtpSettingsToCurrentUser";
            public const string UpdateSmtpSettingsToCurrentUser = $"{Prefix}/UpdateSmtpSettingsToCurrentUser";
            public const string DeleteSmtpSettingForCurrentUser = $"{Prefix}/DeleteSmtpSettingForCurrentUser";
            public const string GetLanguageUser = $"{Prefix}/GetLanguageUser";
            public const string UpdateLanguageSetting = $"{Prefix}/UpdateLanguageSetting";

        }
        public static class AuthenticationRouting
        {
            public const string Prefix = $"{Rule}/Authentication";
            public const string SignUp = $"{Prefix}/SignUp";
            public const string Login = $"{Prefix}/Login";
            public const string RefreshToken = $"{Prefix}/RefreshToken";
            public const string SendCodeResetPassword = $"{Prefix}/SendCodeResetPassword";
            public const string ConfirmCodeResetPassword = $"{Prefix}/ConfirmCodeResetPassword";
            public const string ResetPassword = $"{Prefix}/ResetPassword";
        }
        public static class UserProfileRouting
        {
            public const string Prefix = $"{Rule}/UserProfile";
            public const string CurrentUser = $"{Prefix}/CurrentUser";
            public const string UpdateProfile = $"{Prefix}/UpdateProfile";
            public const string UpdateImageProfile = $"{Prefix}/UpdateImageProfile";
            public const string UpdatePassword = $"{Prefix}/UpdatePassword";
            public const string Logout = $"{Prefix}/Logout";
        }
        public static class UsersManagementRouting
        {
            public const string Prefix = $"{Rule}/UsersManagement";
            public const string GetUsers = $"{Prefix}/GetUsers";
            public const string GetRoles = $"{Prefix}/GetRoles";
            public const string AddUser = $"{Prefix}/AddUser";
            public const string SimpleAddUser = $"{Prefix}/SimpleAddUser";
            public const string UpdateUser = $"{Prefix}/UpdateUser";
            public const string GetUserById = $"{Prefix}/GetUserById/{Id}";
            public const string GetUserPaginatedList = $"{Prefix}/GetUserPaginatedList";
            public const string AssignRoleToUser = $"{Prefix}/AssignRoleToUser";
            public const string DeleteUser = $"{Prefix}/DeleteUser/{Id}";
            public const string ChangePassword = $"{Prefix}/ChangePassword";

        }
        public static class UsersRelativeRouting
        {
            public const string Prefix = $"{Rule}/UsersRelative";
            public const string Get = $"{Prefix}/Get/{Id}";
            public const string Add = $"{Prefix}/Add";
            public const string Update = $"{Prefix}/Update";
            public const string Delete = $"{Prefix}/Delete/{Id}";

        }
        public static class ScannerResultRouting
        {
            public const string Prefix = $"{Rule}/ScannerResult";
            public const string Add = $"{Prefix}/Add";
            public const string Get = $"{Prefix}/Get";


        }
    }
}
