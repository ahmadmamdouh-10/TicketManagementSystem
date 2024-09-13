using Domain.Common;
using Domain.TicketAggregate.Entities;

namespace Domain.TicketAggregate.Events;

public class TicketDeleted : BaseEvent
{
    public TicketDeleted(Ticket ticket)
    {
        Ticket = ticket;
    }

    public Ticket Ticket { get; }
    
}