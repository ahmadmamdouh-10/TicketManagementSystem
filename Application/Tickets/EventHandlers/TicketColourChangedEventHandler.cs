using Domain.TicketAggregate.Events;
using Microsoft.Extensions.Logging;

namespace Application.Tickets.EventHandlers;

public class TicketColourChangedEventHandler : INotificationHandler<TicketColourChangedEvent>
{
    private readonly ILogger<TicketColourChangedEventHandler> _logger;
    
    public TicketColourChangedEventHandler(ILogger<TicketColourChangedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(TicketColourChangedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Ticket Colour Changed to {Colour}", notification.Ticket.Colour);
        return Task.CompletedTask;
    }
}