//using ScanDetector.Api.Base;
//using ScanDetector.Core.Features.AppSetting.Commands.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using static ScanDetector.Data.AppMetaData.Router;

//namespace ScanDetector.Api.Controllers
//{
//    [ApiController]
//    [Authorize]
//    public class AppSettingController : AppBaseController
//    {
//        [HttpGet(AppSettingRouting.SmtpSettingsToCurrentUser)]
//        public async Task<IActionResult> SmtpSettingsToCurrentUser()
//        {
//            var response = await Mediator.Send(new AddSmtpSettingsToCurrentUserCommand());
//            return Result(response);
//        }
//        [HttpGet(AppSettingRouting.GetLanguageUser)]
//        public async Task<IActionResult> GetLanguageUser()
//        {
//            var response = await Mediator.Send(new GetLanguageUserCommand());
//            return Result(response);
//        }
//        [HttpPut(AppSettingRouting.UpdateSmtpSettingsToCurrentUser)]
//        public async Task<IActionResult> UpdateSmtpSettings(UpdateSmtpSettingsCommand items)
//        {
//            var response = await Mediator.Send(items);
//            return Result(response);
//        }
//        [HttpPut(AppSettingRouting.UpdateLanguageSetting)]
//        public async Task<IActionResult> UpdateLanguageSetting(UpdateLanguageSettingCommand items)
//        {
//            var response = await Mediator.Send(items);
//            return Result(response);
//        }
//        [HttpDelete(AppSettingRouting.DeleteSmtpSettingForCurrentUser)]
//        public async Task<IActionResult> DeleteSmtpSettingForCurrentUser()
//        {
//            var response = await Mediator.Send(new DeleteSmtpSettingsToCurrentUserCommand());
//            return Result(response);
//        }
//    }
//}
