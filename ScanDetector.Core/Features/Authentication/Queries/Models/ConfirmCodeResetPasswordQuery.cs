using ScanDetector.Core.Bases;
using MediatR;

namespace ScanDetector.Core.Features.Authentication.Queries.Models
{
    public class ConfirmCodeResetPasswordQuery: IRequest<BaseResponse<string>>
    {
        public string Code { get; set; }
        public string Email { get; set; }
    }
}
