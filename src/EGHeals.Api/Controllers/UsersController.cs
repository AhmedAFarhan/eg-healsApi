using EGHeals.Application.Dtos.Users;
using EGHeals.Application.Features.Users.Commands.Login;
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
        public async Task<IActionResult> LoginAsync(LoginUserDto UserLogin)
        {
            var command = new LoginCommand(UserLogin);

            var result = await sender.Send(command);

            return Ok(result.Response);
        }
    }
}
