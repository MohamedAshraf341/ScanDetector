using ScanDetector.Api.Base;
using ScanDetector.Core.Features.Authentication.Commands.Models;
using ScanDetector.Core.Features.Authentication.Queries.Models;
using Microsoft.AspNetCore.Mvc;
using static ScanDetector.Data.AppMetaData.Router;

namespace ScanDetector.Api.Controllers
{
    [ApiController]

    public class AuthenticationController : AppBaseController
    {
        [HttpPost(AuthenticationRouting.SignUp)]
        public async Task<IActionResult> SignUp(SignUpCommand request)
        {
            var response = await Mediator.Send(request);
            return Result(response);
        }
        [HttpPost(AuthenticationRouting.Login)]
        public async Task<IActionResult> Login(LoginCommand request)
        {
            var response = await Mediator.Send(request);
            return Result(response);
        }
        [HttpPost(AuthenticationRouting.RefreshToken)]
        public async Task<IActionResult> RefreshToken(RefreshTokenCommand request)
        {
            var response = await Mediator.Send(request);
            return Result(response);
        }
        //[HttpPost(AuthenticationRouting.SendCodeResetPassword)]
        //public async Task<IActionResult> SendCodeResetPassword(SendCodeResetPasswordCommand request)
        //{
        //    var response = await Mediator.Send(request);
        //    return Result(response);
        //}
        //[HttpPost(AuthenticationRouting.ConfirmCodeResetPassword)]
        //public async Task<IActionResult> ConfirmCodeResetPassword(ConfirmCodeResetPasswordQuery request)
        //{
        //    var response = await Mediator.Send(request);
        //    return Result(response);
        //}
        //[HttpPost(AuthenticationRouting.ResetPassword)]
        //public async Task<IActionResult> ResetPassword(ResetPasswordCommand request)
        //{
        //    var response = await Mediator.Send(request);
        //    return Result(response);
        //}
    }
}
