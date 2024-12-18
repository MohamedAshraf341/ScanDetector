using ScanDetector.Core.Features.ScannerResult.Commands.Models;

namespace ScanDetector.Core.Mapping.ScannerResult
{
    public partial class ScannerResultProfile
    {
        public void AddScannerResultMapping()
        {
            CreateMap<AddScannerResultCommand, Data.Entities.ScannerResult>();
        }
    }
}
