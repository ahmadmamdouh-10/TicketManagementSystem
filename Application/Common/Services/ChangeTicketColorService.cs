using Hangfire;
using Talabeyah.TicketManagement.Application.Common.Interfaces;
using Talabeyah.TicketManagement.Application.Common.Repositories;
using Talabeyah.TicketManagement.Domain.Enums;

namespace Talabeyah.TicketManagement.Application.Common.Services;

public class ChangeTicketColorService : IChangeTicketColor
{
    private readonly ITicketRepository _repository;
    private readonly IBackgroundJobClient _backgroundJobClient;

    public ChangeTicketColorService(ITicketRepository repository, IBackgroundJobClient backgroundJobClient)
    {
        _repository = repository;
        _backgroundJobClient = backgroundJobClient;
    }

    //this method must be public for the hangfire to work
    // separate it from the Handle method to application/services
    [AutomaticRetry(Attempts = 0)]
    public async Task ChangeTicketColourAsync(int ticketId)
    {
        var ticket = await _repository.GetByIdAsync(ticketId);

        //I want to do this and return so no need for the next operations or checks
        if (ticket.Colour is Color.Red)
        {
            ticket.HandleTicket();
            await _repository.UpdateAsync(ticket, CancellationToken.None);
            return;
        }

        ticket.ChangeColour();
        if (ticket.Colour != Color.None)
        {
            ScheduleChangeTicketColour(ticketId);
        }

        await _repository.UpdateAsync(ticket, CancellationToken.None);
    }
    
    public void ScheduleChangeTicketColour(int ticketId)
    {
        _backgroundJobClient.Schedule<IChangeTicketColor>(
            (eventHandler) => eventHandler.ChangeTicketColourAsync(ticketId), TimeSpan.FromMinutes(15));
    }
}