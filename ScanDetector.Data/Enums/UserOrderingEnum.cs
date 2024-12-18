using System.ComponentModel.DataAnnotations;

namespace ScanDetector.Data.Enums
{
    public enum UserOrderingEnum
    {
        [Display(Name = "Name", Description = "Sort by Name")]
        Name = 1,

        [Display(Name = "Email", Description = "Sort by Email")]
        Email = 2,

        [Display(Name = "Role Id", Description = "Sort by Role Id")]
        RoleId = 3,
    }
}
