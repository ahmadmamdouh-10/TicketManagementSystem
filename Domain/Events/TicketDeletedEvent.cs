using Talabeyah.TicketManagement.Domain.Common;
using Talabeyah.TicketManagement.Domain.Entities;

namespace Talabeyah.TicketManagement.Domain.Events;

public class TicketDeletedEvent : BaseEvent
{
    public TicketDeletedEvent(Ticket ticket)
    {
        Ticket = ticket;
    }

    public Ticket Ticket { get; }
    
}