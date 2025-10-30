using EGHeals.Application.Dtos.Users.Requests;
using EGHeals.Application.Features.Users.Commands.Activate;
using EGHeals.Application.Features.Users.Commands.Deactivate;
using EGHeals.Application.Features.Users.Commands.Delete;
using EGHeals.Application.Features.Users.Commands.Login;
using EGHeals.Application.Features.Users.Commands.RegisterSubUser;
using EGHeals.Application.Features.Users.Commands.UpdateSubUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EGHeals.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(ISender sender) : ControllerBase
    {
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginUserRequestDto userLogin)
        {
            var command = new LoginCommand(userLogin);

            var result = await sender.Send(command);

            return Ok(result.Response);
        }

        [Route("RegisterSubUser")]
        [HttpPost]
        public async Task<IActionResult> RegisterSubUserAsync(RegisterSubUserRequestDto registerSubUserRequestDto)
        {
            var command = new RegisterSubUserCommand(registerSubUserRequestDto);

            var result = await sender.Send(command);

            return Ok(result.Response);
        }

        [Route("UpdateSubUser/{id:guid}")]
        [HttpPut]
        public async Task<IActionResult> UpdateSubUserAsync(Guid id, UpdateSubUserRequestDto updateSubUserRequestDto)
        {
            var command = new UpdateSubUserCommand(id, updateSubUserRequestDto);

            var result = await sender.Send(command);

            return Ok(result.Response);
        }

        //[Route("UpdateSubUserRoles/{id:guid}")]
        //[HttpPut]
        //public async Task<IActionResult> UpdateSubUserRolesAsync(Guid id, UpdateSubUserRequestDto updateSubUserRequestDto)
        //{
        //    var command = new UpdateSubUserCommand(id, updateSubUserRequestDto);

        //    var result = await sender.Send(command);

        //    return Ok(result.Response);
        //}

        [Route("Delete/{id:guid}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var command = new DeleteUserCommand(id);

            var result = await sender.Send(command);

            return Ok(result.Response);
        }

        [Route("Activate/{id:guid}")]
        [HttpPut]
        public async Task<IActionResult> ActivateAsync(Guid id)
        {
            var command = new ActivateUserCommand(id);

            var result = await sender.Send(command);

            return Ok(result.Response);
        }

        [Route("Deactivate/{id:guid}")]
        [HttpPut]
        public async Task<IActionResult> DeactivateAsync(Guid id)
        {
            var command = new DeactivateUserCommand(id);

            var result = await sender.Send(command);

            return Ok(result.Response);
        }
    }
}
