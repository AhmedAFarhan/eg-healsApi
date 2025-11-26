using BuildingBlocks.DataAccessAbstraction.Queries;
using EGHeals.Application.Features.Shared.Users.Queries.GetRolesByTenant;
using EGHeals.Domain.Models.Shared.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EGHeals.Api.Controllers.Shared
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController(ISender sender) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromBody] QueryOptions<Role> query)
        {
            var command = new GetRolesByTenantQuery(query);

            var result = await sender.Send(command);

            return Ok(result.Response);
        }
    }
}
