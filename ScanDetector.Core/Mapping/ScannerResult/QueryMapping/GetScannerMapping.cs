using ScanDetector.Core.Features.ScannerResult.Queries.Results;

namespace ScanDetector.Core.Mapping.ScannerResult
{
    public partial class ScannerResultProfile
    {
        public void GetScannerMapping()
        {
            CreateMap<Data.Entities.ScannerResult, GetScannerResponse>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName));
        }
    }
}
