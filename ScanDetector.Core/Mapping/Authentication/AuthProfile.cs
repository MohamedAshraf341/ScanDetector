

using AutoMapper;

namespace ScanDetector.Core.Mapping.Authentication
{
    public partial class AuthenticationProfile : Profile
    {
        public AuthenticationProfile() 
        {
            SignUpMapping();
            TokenMapping();
        }
    }
}
