using Hangfire;
using Microsoft.Extensions.Logging;
using Talabeyah.TicketManagement.Application.Common.Interfaces;
using Talabeyah.TicketManagement.Domain.Enums;
using Talabeyah.TicketManagement.Domain.Events;

namespace Talabeyah.TicketManagement.Application.Tickets.EventHandlers;

public class TicketCreatedEventHandler : INotificationHandler<TicketCreatedEvent>
{
    private readonly ILogger<TicketCreatedEventHandler> _logger;

    private readonly IBackgroundJobClient _backgroundJobClient;


    public TicketCreatedEventHandler(ILogger<TicketCreatedEventHandler> logger,
        IBackgroundJobClient backgroundJobClient)
    {
        _logger = logger;
        _backgroundJobClient = backgroundJobClient;
    }

    public Task Handle(TicketCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Ticket Created with Id {TicketId}", notification.Ticket.Id);

        //background service of hangfire
        // Schedule the first color change to yellow after 15 minutes
        _backgroundJobClient.Schedule<IChangeTicketColor>(
            (eventHandler) =>
                eventHandler.ChangeTicketColourAsync(notification.Ticket.Id, Color.Yellow),
            TimeSpan.FromMinutes(15));

        return Task.CompletedTask;
    }
}