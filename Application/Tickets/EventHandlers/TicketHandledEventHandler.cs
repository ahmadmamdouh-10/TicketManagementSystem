using Microsoft.Extensions.Logging;
using Talabeyah.TicketManagement.Domain.Events;

namespace Talabeyah.TicketManagement.Application.Tickets.EventHandlers;

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