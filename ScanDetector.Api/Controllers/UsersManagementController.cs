using ScanDetector.Api.Base;
using ScanDetector.Core.Features.Role.Queries.Models;
using ScanDetector.Core.Features.User.Commands.Models;
using ScanDetector.Core.Features.User.Queries.Models;
using ScanDetector.Data.AppMetaData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static ScanDetector.Data.AppMetaData.Router;
using ScanDetector.Core.Features.UserProfile.Commands.Models;
using ScanDetector.Service.Implementations;
using ScanDetector.Core.Middleware;

namespace ScanDetector.Api.Controllers
{
    [ApiController]
    [Authorize]

    public class UsersManagementController : AppBaseController
    {
        //[CustomAuthorize(AppPermission.GetUser)]
        //[HttpGet(UsersManagementRouting.GetUsers)]
        //public async Task<IActionResult> GetUsers()
        //{
        //    var response = await Mediator.Send(new GetUsersQuery());
        //    return Result(response);
        //}
        [CustomAuthorize(AppPermission.GetUser)]
        [HttpPost(UsersManagementRouting.GetUsers)]
        public async Task<IActionResult> GetUserPaginatedList([FromQuery] GetUserPaginatedListQuery request)
        {
            var response = await Mediator.Send(request);
            return ResultPagination(response);
        }
        [CustomAuthorize(AppPermission.GetUser)]
        [HttpGet(UsersManagementRouting.GetUserById)]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var response = await Mediator.Send(new GetUserByIdQuery() { Id = id });
            return Result(response);
        }
        [CustomAuthorize(AppPermission.EditUser, AppPermission.AddUser)]
        [HttpGet(UsersManagementRouting.GetRoles)]
        public async Task<IActionResult> GetRoles()
        {
            var response = await Mediator.Send(new GetRolesQuery());
            return Result(response);
        }
        //[HttpPost(UsersManagementRouting.AddUser)]
        //public async Task<IActionResult> AddUser(AddUserCommand request)
        //{
        //    var response = await Mediator.Send(request);
        //    return Result(response);
        //}
        [CustomAuthorize(AppPermission.AddUser)]
        [HttpPost(UsersManagementRouting.AddUser)]
        public async Task<IActionResult> SimpleAddUser(SimpleAddUserCommand request)
        {
            var response = await Mediator.Send(request);
            return Result(response);
        }
        [CustomAuthorize(AppPermission.EditUser)]
        [HttpPut(UsersManagementRouting.UpdateUser)]
        public async Task<IActionResult> UpdateUser(UpdateUserCommand request)
        {
            var response = await Mediator.Send(request);
            return Result(response);
        }

        [CustomAuthorize(AppPermission.EditUser)]
        [HttpPost(UsersManagementRouting.AssignRoleToUser)]
        public async Task<IActionResult> AssignRoleToUser(AssignRoleToUserCommand request)
        {
            var response = await Mediator.Send(request);
            return Result(response);
        }
        [CustomAuthorize(AppPermission.DeleteUser)]
        [HttpDelete(UsersManagementRouting.DeleteUser)]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var response = await Mediator.Send(new DeleteUserCommand { Id = id });
            return Result(response);
        }
        [CustomAuthorize(AppPermission.EditUser)]
        [HttpPut(UsersManagementRouting.ChangePassword)]
        public async Task<IActionResult> ChangePassword(Core.Features.User.Commands.Models.ChangePasswordCommand request)
        {
            var response = await Mediator.Send(request);
            return Result(response);
        }
    }
}
