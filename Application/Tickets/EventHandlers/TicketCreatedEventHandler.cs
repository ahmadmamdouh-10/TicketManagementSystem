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
    private readonly IChangeTicketColor _changeTicketColor;


    public TicketCreatedEventHandler(ILogger<TicketCreatedEventHandler> logger,
        IBackgroundJobClient backgroundJobClient, IChangeTicketColor changeTicketColor)
    {
        _logger = logger;
        _backgroundJobClient = backgroundJobClient;
        _changeTicketColor = changeTicketColor;
    }

    public Task Handle(TicketCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Ticket Created with Id {TicketId}", notification.Ticket.Id);

        // Schedule the first color change to yellow after 15 minutes
        _changeTicketColor.ScheduleChangeTicketColour(notification.Ticket.Id);

        return Task.CompletedTask;
    }
}