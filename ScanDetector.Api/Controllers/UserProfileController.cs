using ScanDetector.Api.Base;
using ScanDetector.Core.Features.UserProfile.Commands.Models;
using ScanDetector.Core.Features.UserProfile.Queries.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ScanDetector.Data.AppMetaData.Router;

namespace ScanDetector.Api.Controllers
{
    [ApiController]
    [Authorize]
    public class UserProfileController : AppBaseController
    {
        [HttpGet(UserProfileRouting.CurrentUser)]
        public async Task<IActionResult> CurrentUser()
        {
            var response = await Mediator.Send(new CurrentUserQuery());
            return Result(response);
        }
        [HttpPut(UserProfileRouting.UpdateProfile)]
        public async Task<IActionResult> UpdateProfile(UpdateProfileCommand request)
        {
            var response = await Mediator.Send(request);
            return Result(response);
        }
        [HttpPut(UserProfileRouting.UpdateImageProfile)]
        public async Task<IActionResult> UpdateImageProfile([FromForm]ChangePhotoProfileCommand request)
        {
            var response = await Mediator.Send(request);
            return Result(response);
        }
        [HttpPut(UserProfileRouting.UpdatePassword)]
        public async Task<IActionResult> UpdatePassword(ChangePasswordCommand request)
        {
            var response = await Mediator.Send(request);
            return Result(response);
        }
        [HttpPost(UserProfileRouting.Logout)]
        public async Task<IActionResult> Logout(LogoutCommand request)
        {
            var response = await Mediator.Send(request);
            return Result(response);
        }
        //[HttpGet(UserProfileRouting.Prefix + "claims")]
        //public IActionResult GetClaims()
        //{
        //    // Check if there are any claims present in the HttpContext
        //    var claims = HttpContext?.User?.Claims;

        //    if (claims == null || !claims.Any())
        //    {
        //        // Return a 401 Unauthorized response if no claims are found
        //        return Unauthorized("No claims found. Please ensure a valid Bearer token is sent.");
        //    }

        //    // Create a dictionary to hold claim types and their values
        //    var claimsDictionary = claims.ToDictionary(claim => claim.Type, claim => claim.Value);

        //    // Return the claims as a JSON response
        //    return Ok(claimsDictionary);
        //}


    }
}
