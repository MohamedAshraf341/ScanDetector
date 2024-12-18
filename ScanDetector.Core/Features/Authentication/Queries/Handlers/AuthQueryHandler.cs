using ScanDetector.Core.Bases;
using ScanDetector.Core.Features.Authentication.Queries.Models;
using ScanDetector.Infrastructure.Abstracts.Base;
using MediatR;
using Microsoft.Extensions.Localization;
using ScanDetector.Core.Resources;

namespace ScanDetector.Core.Features.Authentication.Queries.Handlers
{
    public class AuthenticationQueryHandler : ResponseHandler,
         IRequestHandler<ConfirmCodeResetPasswordQuery, BaseResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public AuthenticationQueryHandler(IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> stringLocalizer):base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<string>> Handle(ConfirmCodeResetPasswordQuery request, CancellationToken cancellationToken)
        {
            var code=await _unitOfWork.UserCodes.GetByEmailAndCode(request.Email,request.Code);
            if (code == null)
                return NotFound<string>();
            if (code.ExpiresOn < DateTime.Now)
                return BadRequest<string>("Code is expired");
            return Success("Confirmed Code");
        }
    }
}
