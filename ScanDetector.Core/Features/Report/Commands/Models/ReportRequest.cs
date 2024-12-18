using MediatR;
using ScanDetector.Core.Bases;
using ScanDetector.Data.Enums;
namespace ScanDetector.Core.Features.Report.Commands.Models
{
    public class ReportRequest: IRequest<BaseResponse<byte[]>>
    {
        public ReportType ReportType { get; set; }
        public string Title { get; set; }
        public string CultureCode { get; set; } // e.g., "ar-EG" or "en-US"
        public Dictionary<string, string> Headers { get; set; } // Header mappings
        public List<Dictionary<string, object>> Items { get; set; } // Rows of data
    }
}
