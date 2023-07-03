using DomainEvents.Application.Commands;
using DomainEvents.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DomainEvents.Api.Controllers;

[ApiController]
[Route("people")]
public class PeopleController : ControllerBase
{
    private readonly IMediator _mediator;

    public PeopleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var people = await _mediator.Send(new GetPeople());
        return Ok(people);
    }
    
    [HttpPut]
    public async Task<IActionResult> Update(UpdatePerson request)
    {
        await _mediator.Send(request);
        return Ok();
    }
}