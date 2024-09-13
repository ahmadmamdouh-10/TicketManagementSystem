using Domain.Common;
using Domain.TicketAggregate.Entities;

namespace Domain.TicketAggregate.Events;

public class TicketHandledEvent : BaseEvent
{
    public TicketHandledEvent(Ticket ticket)
    {
        Ticket = ticket;
    }

    public Ticket Ticket { get; }
}