

using ScanDetector.Data.Entities.Authentication;

namespace ScanDetector.Data.AppMetaData
{
    public class PermissionData
    {
        #region List
        public static readonly List<ModulePermission> allModulePermission = new();
        public static readonly List<Permission> allPermission = new();
        #endregion

        #region Modules Data
        public static readonly ModulePermission User = NewModulePermission(Guid.Parse("df615911-1d00-4852-8d6f-f722b0e6a001"), "Users", "المستخدمين");
        public static readonly ModulePermission Settings = NewModulePermission(Guid.Parse("3bc7ef1b-401b-4258-922b-ef58dd5803d7"), "Settings", "الاعدادات");

        #endregion
        #region Permission Data
        public static readonly Permission GetUser = NewPermission(Guid.Parse("c714a459-92a5-41d6-8d72-de359f760489"), User.Id, "Get User", "جلب المستخدمين");
        public static readonly Permission AddUser = NewPermission(Guid.Parse("df615911-1d00-4852-8d6f-f722b0e6a001"), User.Id, "Add User", "اضافة مستخدم");
        public static readonly Permission EditUser = NewPermission(Guid.Parse("9d6ef371-407d-4441-9da7-3c7d8bd7225e"), User.Id, "Edit User", "تعديل مستخدم");
        public static readonly Permission DeleteUser = NewPermission(Guid.Parse("a8fe7eb1-5f8f-44c3-84ed-031a4474b5b8"), User.Id, "Delete User", "حذف مستخدم");

        public static readonly Permission EmailSetting = NewPermission(Guid.Parse("fad36dd6-14a9-4e67-ba92-3b9dd1e8634c"), Settings.Id, "Email Setting", "اعدادات الايميل");

        #endregion
        protected static ModulePermission NewModulePermission(Guid id, string enName, string arName)
        {
            var r = new ModulePermission { Id = id, NameEn = enName, NameAr = arName };
            allModulePermission.Add(r);
            return r;
        }
        protected static Permission NewPermission(Guid id, Guid moduleId, string enName, string arName)
        {
            var r = new Permission { Id = id, ModulePermissionId = moduleId, NameEn = enName, NameAr = arName };
            allPermission.Add(r);
            return r;
        }
    }

}
