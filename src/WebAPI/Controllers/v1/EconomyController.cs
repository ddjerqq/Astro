using Application.Economy.Commands;
using Astro.Generated;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1;

[Route("/api/v1/transfer")]
public class EconomyController(IMediator mediator) : ApiController
{
    [HttpPost("/send/{ReciverId}")]
    public Task<bool> Send(UserId ReciverId)
    {
        throw new System.NotImplementedException();
    }

}