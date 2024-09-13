using Microsoft.Extensions.Logging;
using Talabeyah.TicketManagement.Domain.Events;

namespace Talabeyah.TicketManagement.Application.Tickets.EventHandlers;

//it's not needed as this even occurs in the TicketCreatedEventHandler with ChangeTicketColor Service
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