using Application.Economy.Commands;
using Astro.Generated;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1;

[Route("/api/v1/economy")]
public class EconomyController(IMediator mediator) : ApiController
{
    [HttpPost("send")]
    public async Task<ActionResult> Send([FromBody] BalanceTransactionCommand command, CancellationToken ct)

    {
        var resp = await mediator.Send(command, ct);
        if (resp)
        {
            return Ok("Success");
        }
        return Unauthorized("INVALID"); ;

    }

} 