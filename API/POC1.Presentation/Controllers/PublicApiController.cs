using MediatR;
using Microsoft.AspNetCore.Mvc;
using POC1.Application.Dtos;
using POC1.Application.Queries.ApiBlobs;
using POC1.Application.Queries.ApiLogs;

namespace POC1.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PublicApiController : ControllerBase
{

    private readonly IMediator _mediator;
    public PublicApiController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("logs")]
    public async Task<ActionResult<ApiLogsResponse>> GetLogs([FromQuery] GetApiLogsQuery getApiLogsQuery)
    {
        var logs = await _mediator.Send(getApiLogsQuery);
        if (logs.Success)
        {
            return Ok(logs);
        }

        return BadRequest(logs.Message);
    }
    [HttpGet]
    [Route("payload")]
    public async Task<ActionResult<ApiBlobResponse>> GetPayload([FromQuery] GetApiBlobQuery getApiBlobQuery)
    {
        var payload =  await _mediator.Send(getApiBlobQuery);
        if (payload.Success)
        {
            return Ok(payload);
        }

        return NotFound(payload.Message);
    }
}
