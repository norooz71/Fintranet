using Application.Contract.Queries.Toll;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class TollController : ApiControllerBase
{

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<int>> Calculate([FromQuery] TollCalculatorInputQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

}
