using ScanDetector.Data.Entities.Authentication;

namespace ScanDetector.Data.AppMetaData
{
    public static class UserData
    {
        public static readonly User Admin = new User
        {
            Id= Guid.Parse("0660795a-7fb7-498c-af99-0fcce6fd6318"),
            FirstName = "Admin",
            LastName = "",
            Email= "admin",
            HashPassword= BCrypt.Net.BCrypt.HashPassword("admin"),
            RoleId= RoleData.Admin.Id,
        };
        public static readonly User User = new User
        {
            Id = Guid.Parse("d26ee7bb-49ba-4174-9d64-83d8934c5920"),
            FirstName = "User",
            LastName = "",
            Email = "user",
            HashPassword = BCrypt.Net.BCrypt.HashPassword("user"),
            RoleId = RoleData.User.Id,
        };

    }
}
