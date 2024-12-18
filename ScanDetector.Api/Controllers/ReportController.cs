//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using ScanDetector.Api.Base;
//using ScanDetector.Core.Features.Report.Commands.Models;
//using ScanDetector.Core.Features.UserProfile.Commands.Models;
//using ScanDetector.Data.AppMetaData;
//using static ScanDetector.Data.AppMetaData.Router;

//namespace ScanDetector.Api.Controllers
//{
//    [ApiController]
//    [Authorize]
//    public class ReportController : AppBaseController
//    {
//        [HttpPost(ReportRouting.Generate)]
//        public async Task<IActionResult> Generate(ReportRequest request)
//        {
//            var response = await Mediator.Send(request);
//            return Result(response);
//        }
//    }
//}
