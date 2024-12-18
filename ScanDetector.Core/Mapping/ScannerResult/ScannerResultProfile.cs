using AutoMapper;

namespace ScanDetector.Core.Mapping.ScannerResult
{
    public partial class ScannerResultProfile: Profile
    {
        public ScannerResultProfile() 
        {
            AddScannerResultMapping();
            GetScannerMapping();
        }
    }
}
