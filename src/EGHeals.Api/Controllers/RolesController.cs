using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EGHeals.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController(ISender sender) : ControllerBase
    {

    }
}
