using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScanDetector.Api.Base;
using ScanDetector.Core.Features.UsersRelative.Commands.Models;
using ScanDetector.Core.Features.UsersRelative.Queries.Models;
using static ScanDetector.Data.AppMetaData.Router;

namespace ScanDetector.Api.Controllers
{
    [ApiController]
    [Authorize]
    public class UsersRelativeController : AppBaseController
    {
        [HttpPost(UsersRelativeRouting.Add)]
        public async Task<IActionResult> Add(AddUsersRelativeCommand items)
        {
            var response = await Mediator.Send(items);
            return Result(response);
        }
        [HttpPut(UsersRelativeRouting.Update)]
        public async Task<IActionResult> Update(UpdateUsersRelativeCommand items)
        {
            var response = await Mediator.Send(items);
            return Result(response);
        }
        [HttpGet(UsersRelativeRouting.Get)]
        public async Task<IActionResult> Update( Guid id)
        {
            var response = await Mediator.Send(new GetUsersRelativeQuery { FilterByUserId=id});
            return Result(response);
        }
        [HttpDelete(UsersRelativeRouting.Delete)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await Mediator.Send(new DeleteUsersRelativeCommand { Id = id });
            return Result(response);
        }
    }
}
