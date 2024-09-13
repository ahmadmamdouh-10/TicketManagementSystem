using Application.Common.Models;
using Application.Tickets.Commands.CreateTicket;
using Application.Tickets.Commands.DeleteTicket;
using Application.Tickets.Commands.HandleTicket;
using Application.Tickets.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class TicketController : ControllerBase
{
    [HttpGet]
    public Task<PaginatedList<TicketDto>> GetTicketsWithPagination([FromServices] ISender sender,
        [FromQuery] GetTicketsWithPaginationQuery query)
    {
        return sender.Send(query);
    }

    [HttpPost]
    public Task<int> Create([FromServices] ISender sender, [FromBody] CreateTicketCommand command)
    {
        return sender.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromServices] ISender sender, int id, [FromBody] HandleTicket command)
    {
        if (id != command.Id) return BadRequest();

        await sender.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromServices] ISender sender, int id,
        [FromBody] DeleteTicketCommand command)
    {
        if (id != command.Id) return BadRequest();
        await sender.Send(command);
        return NoContent();
    }
}