using Domain.Common;
using Domain.TicketAggregate.Entities;

namespace Domain.TicketAggregate.Events;

public class TicketColourChangedEvent : BaseEvent
{
    public TicketColourChangedEvent(Ticket ticket)
    {
        Ticket = ticket;
    }

    public Ticket Ticket { get; }
}