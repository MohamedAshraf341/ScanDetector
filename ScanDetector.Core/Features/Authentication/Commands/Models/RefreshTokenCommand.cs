using ScanDetector.Core.Bases;
using ScanDetector.Core.Features.Authentication.Commands.Results;
using MediatR;

namespace ScanDetector.Core.Features.Authentication.Commands.Models
{
    public class RefreshTokenCommand:IRequest<BaseResponse<TokenResponse>>
    {
        public string RefreshToken { get; set; }
    }
}
