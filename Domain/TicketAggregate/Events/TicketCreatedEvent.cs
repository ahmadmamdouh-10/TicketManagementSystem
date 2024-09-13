using Domain.Common;
using Domain.TicketAggregate.Entities;

namespace Domain.TicketAggregate.Events;

public class TicketCreatedEvent : BaseEvent
{
    public TicketCreatedEvent(Ticket ticket)
    {
        Ticket = ticket;
    }

    public Ticket Ticket { get; }
}