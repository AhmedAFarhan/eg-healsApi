
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EGHeals.Api.Controllers.RadiologyCenter
{
    [ApiController]
    [Route("api/[controller]")]
    public class RadiologyExaminationsController(ISender sender) : ControllerBase
    {
        //[Route("GetExaminationsByOwnership")]
        //[HttpPost]
        //public async Task<IActionResult> GetExaminationsByOwnershipAsync([FromBody] QueryOptions<RadiologyExaminationResponseDto> options)
        //{
        //    var command = new GetExaminationsByOwnershipQuery(options);

        //    var result = await sender.Send(command);

        //    return Ok(result.Response);
        //}
    }
}
