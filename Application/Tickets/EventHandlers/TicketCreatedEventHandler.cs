using Domain.TicketAggregate.Enums;
using Domain.TicketAggregate.Events;
using Domain.TicketAggregate.Services;
using Hangfire;
using Microsoft.Extensions.Logging;

namespace Application.Tickets.EventHandlers;

public class TicketCreatedEventHandler : INotificationHandler<TicketCreatedEvent>
{
    private readonly ILogger<TicketCreatedEventHandler> _logger;
    private readonly ITicketService _ticketService;


    public TicketCreatedEventHandler(ILogger<TicketCreatedEventHandler> logger, ITicketService ticketService)
    {
        _logger = logger;
        _ticketService = ticketService;
    }

    public Task Handle(TicketCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Ticket Created with Id {TicketId}", notification.Ticket.Id);

        // Schedule the first color change to yellow after 15 minutes
        BackgroundJob.Schedule(() => ChangeTicketColour(notification.Ticket.Id, Colour.Yellow),
            TimeSpan.FromMinutes(15));

        return Task.CompletedTask;
    }

    [AutomaticRetry(Attempts = 0)]
    private async Task ChangeTicketColour(int ticketId, Colour colour)
    {
        _ticketService.ChangeTicketColourAsync(ticketId, colour, CancellationToken.None).Wait();

        // Schedule the next color change if applicable
        Colour nextColour = colour switch
        {
            Colour.Yellow => Colour.Green,
            Colour.Green => Colour.Red,
            _ => Colour.None
        };

        if (nextColour != Colour.None)
        {
            BackgroundJob.Schedule(() => ChangeTicketColour(ticketId, nextColour), TimeSpan.FromMinutes(15));
        }
        else if (colour == Colour.Red)
        {
            // Handle the ticket when the color is red
            await _ticketService.HandleTicketAsync(ticketId, CancellationToken.None);
        }
    }
}