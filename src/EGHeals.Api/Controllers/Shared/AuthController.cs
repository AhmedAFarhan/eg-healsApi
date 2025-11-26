using EGHeals.Application.Dtos.Shared.Users.Requests;
using EGHeals.Application.Features.Shared.Users.Commands.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EGHeals.Api.Controllers.Shared
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(ISender sender) : ControllerBase
    {
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserRequestDto userLogin)
        {
            var command = new LoginCommand(userLogin);

            var result = await sender.Send(command);

            return Ok(result.Response);
        }
    }
}
