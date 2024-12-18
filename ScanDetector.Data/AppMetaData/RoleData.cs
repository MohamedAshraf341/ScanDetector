using ScanDetector.Data.Entities.Authentication;

namespace ScanDetector.Data.AppMetaData
{
    public static class RoleData
    {
        public static readonly Role Admin = new Role() { Id = Guid.Parse("7c37105b-1e3e-4b69-a244-f337c857e44e"), NameEn = "Admin", NameAr = "أدمن" };
        public static readonly Role User = new Role() { Id = Guid.Parse("82321084-b653-476c-b2c1-18a20a27e28e"), NameEn = "User", NameAr = "مستخدم" };

    }
}
