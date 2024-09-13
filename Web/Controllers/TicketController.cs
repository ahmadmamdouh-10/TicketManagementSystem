using Microsoft.AspNetCore.Mvc;
using Talabeyah.TicketManagement.Application.Common.Models;
using Talabeyah.TicketManagement.Application.Tickets.Commands.CreateTicket;
using Talabeyah.TicketManagement.Application.Tickets.Commands.DeleteTicket;
using Talabeyah.TicketManagement.Application.Tickets.Commands.HandleTicket;
using Talabeyah.TicketManagement.Application.Tickets.Queries;

namespace Talabeyah.TicketManagement.Web.Controllers;

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