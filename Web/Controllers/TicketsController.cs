using Microsoft.AspNetCore.Mvc;
using Talabeyah.TicketManagement.Application.Common.Models;
using Talabeyah.TicketManagement.Application.Tickets.Commands.CreateTicket;
using Talabeyah.TicketManagement.Application.Tickets.Commands.DeleteTicket;
using Talabeyah.TicketManagement.Application.Tickets.Commands.HandleTicket;
using Talabeyah.TicketManagement.Application.Tickets.Queries;
using Talabeyah.TicketManagement.Web.Infrastructure;

namespace Talabeyah.TicketManagement.Web.Controllers;

[ApiController]
[Route("api/tickets")]
public class TicketsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TicketsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetTicketsWithPagination(
        [FromQuery] GetTicketsWithPaginationQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<int> Create([FromBody] CreateTicketCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] HandleTicketCommand command)
    {
        if (id != command.Id) return BadRequest();

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id,
        [FromBody] DeleteTicketCommand command)
    {
        if (id != command.Id) return BadRequest();
        await _mediator.Send(command);
        return NoContent();
    }
}