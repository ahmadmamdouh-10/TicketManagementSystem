using Domain.TicketAggregate.Events;
using Microsoft.Extensions.Logging;

namespace Application.Tickets.EventHandlers;

public class TicketHandledEventHandler : INotificationHandler<TicketHandledEvent>
{
    private readonly ILogger<TicketHandledEventHandler> _logger;
    
    public TicketHandledEventHandler(ILogger<TicketHandledEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(TicketHandledEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Ticket with {Id} Handled", notification.Ticket.Id);
        return Task.CompletedTask;
    }
}