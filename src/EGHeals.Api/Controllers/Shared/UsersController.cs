using BuildingBlocks.DataAccessAbstraction.Queries;
using EGHeals.Application.Dtos.Shared.Users.Requests;
using EGHeals.Application.Features.Shared.Users.Commands.Activate;
using EGHeals.Application.Features.Shared.Users.Commands.Deactivate;
using EGHeals.Application.Features.Shared.Users.Commands.Delete;
using EGHeals.Application.Features.Shared.Users.Commands.RegisterSubUser;
using EGHeals.Application.Features.Shared.Users.Commands.UpdateSubUser;
using EGHeals.Application.Features.Shared.Users.Queries.GetSubUsersByOwnership;
using EGHeals.Domain.Models.Shared.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EGHeals.Api.Controllers.Shared
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController(ISender sender) : ControllerBase
    {
        [Route("GetSubUsers")]
        [HttpPost]
        public async Task<IActionResult> GetSubUsersAsync([FromBody] QueryOptions<User> query)
        {
            var command = new GetSubUsersByTenantQuery(query);

            var result = await sender.Send(command);

            return Ok(result.Response);
        }

        //[Route("GetSubUserPermissions/{id:guid}")]
        //[HttpPost]
        //public async Task<IActionResult> GetSubUserPermissionsAsync(Guid id)
        //{
        //    var command = new GetSubUserPermissionsByOwnershipQuery(id);

        //    var result = await sender.Send(command);

        //    return Ok(result.Response);
        //}

        [Route("RegisterSubUser")]
        [HttpPost]
        public async Task<IActionResult> RegisterSubUserAsync([FromBody] RegisterSubUserRequestDto registerSubUserRequestDto)
        {
            var command = new RegisterSubUserCommand(registerSubUserRequestDto);

            var result = await sender.Send(command);

            return Ok(result.Response);
        }

        [Route("UpdateSubUser/{id:guid}")]
        [HttpPut]
        public async Task<IActionResult> UpdateSubUserAsync(Guid id, [FromBody] UpdateSubUserRequestDto updateSubUserRequestDto)
        {
            var command = new UpdateSubUserCommand(id, updateSubUserRequestDto);

            var result = await sender.Send(command);

            return Ok(result.Response);
        }

        //[Route("UpdateSubUserPermissions/{id:guid}")]
        //[HttpPut]
        //public async Task<IActionResult> UpdateSubUserPermissionsAsync(Guid id, [FromBody] IEnumerable<AddUserPermissionRequestDto> permissions)
        //{
        //    var command = new UpdateSubUserPermissionsCommand(id, permissions);

        //    var result = await sender.Send(command);

        //    return Ok(result.Response);
        //}

        [Route("{id:guid}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var command = new DeleteUserCommand(id);

            var result = await sender.Send(command);

            return Ok(result.Response);
        }

        [Route("{id:guid}/Activate")]
        [HttpPut]
        public async Task<IActionResult> ActivateAsync(Guid id)
        {
            var command = new ActivateUserCommand(id);

            var result = await sender.Send(command);

            return Ok(result.Response);
        }

        [Route("{id:guid}/Deactivate")]
        [HttpPut]
        public async Task<IActionResult> DeactivateAsync(Guid id)
        {
            var command = new DeactivateUserCommand(id);

            var result = await sender.Send(command);

            return Ok(result.Response);
        }
    }
}
