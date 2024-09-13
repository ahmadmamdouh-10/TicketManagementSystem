using Hangfire;
using Talabeyah.TicketManagement.Application.Common.Interfaces;
using Talabeyah.TicketManagement.Application.Common.Repositories;
using Talabeyah.TicketManagement.Domain.Enums;

namespace Talabeyah.TicketManagement.Application.Common.Services;

public class ChangeTicketColor : IChangeTicketColor
{
    private readonly ITicketRepository _repository;
    private readonly IBackgroundJobClient _backgroundJobClient;

    public ChangeTicketColor(ITicketRepository repository, IBackgroundJobClient backgroundJobClient)
    {
        _repository = repository;
        _backgroundJobClient = backgroundJobClient;
    }

    //this method must be public for the hangfire to work
    // separate it from the Handle method to application/services
    [AutomaticRetry(Attempts = 0)]
    public async Task ChangeTicketColourAsync(int ticketId, Color color)
    {
        var ticket = await _repository.GetByIdAsync(ticketId);

        //I want to do this and return so no need for the next operations or checks
        if (color is Color.Red)
        {
            ticket.HandleTicket();
            await _repository.UpdateAsync(ticket, CancellationToken.None);
            return;
        }
        
        ticket.ChangeColour(color);
        if (ticket.Colour != Color.None)
        {
            _backgroundJobClient.Schedule(() => ChangeTicketColourAsync(ticketId, ticket.Colour), TimeSpan.FromMinutes(15));
        }
        await _repository.UpdateAsync(ticket, CancellationToken.None);
    }
}