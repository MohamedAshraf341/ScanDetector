using System.ComponentModel.DataAnnotations;

namespace ScanDetector.Data.Enums
{
    public enum ReportType
    {
        [Display(Name = "Excel", Description = "Report by Excel")]
        Excel = 1,
        [Display(Name = "Pdf", Description = "Report by Pdf")]
        Pdf = 2,
    }
}
