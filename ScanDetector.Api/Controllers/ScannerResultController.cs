using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScanDetector.Api.Base;
using ScanDetector.Core.Features.ScannerResult.Commands.Models;
using ScanDetector.Core.Features.ScannerResult.Queries.Models;
using static ScanDetector.Data.AppMetaData.Router;

namespace ScanDetector.Api.Controllers
{
    [ApiController]
    public class ScannerResultController : AppBaseController
    {
        [HttpPost(ScannerResultRouting.Add)]
        public async Task<IActionResult> Add(AddScannerResultCommand items)
        {
            var response = await Mediator.Send(items);
            return Result(response);
        }
        [Authorize]
        [HttpPost(ScannerResultRouting.Get)]
        public async Task<IActionResult> Get(GetScannerResultQuery items)
        {
            var response = await Mediator.Send(items);
            return Result(response);
        }
    }
}
