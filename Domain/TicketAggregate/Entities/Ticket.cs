using Domain.Common;
using Domain.TicketAggregate.Enums;
using Domain.TicketAggregate.Events;
using Domain.TicketAggregate.ValueObjects;

namespace Domain.TicketAggregate.Entities;

public class Ticket : BaseAuditableEntity, IAgreggateRoot
{
    public PhoneNumber PhoneNumber { get; private set; }
    public Location Location { get; private set; }
    public bool IsHandled { get; private set; }
    public Colour Colour { get; private set; }

    // Private constructor to enforce the use of the factory method
    private Ticket(PhoneNumber phoneNumber, Location location)
    {
        PhoneNumber = phoneNumber;
        Location = location;
        Colour = Colour.White;
        IsHandled = false;
    }

    // Static factory method to create a Ticket and add the domain event
    public static Ticket Create(string phoneNumber, Location location)
    {
        var ticket = new Ticket(PhoneNumber.Create(phoneNumber), location);
        ticket.AddDomainEvent(new TicketCreatedEvent(ticket));
        return ticket;
    }

    public void HandleTicket()
    {
        if (IsHandled)
            throw new InvalidOperationException("Ticket is already handled");

        IsHandled = true;
        AddDomainEvent(new TicketHandledEvent(this));
    }

    public void ChangeColour(Colour newColour)
    {
        if (newColour == Colour.None)
            throw new ArgumentException("Invalid colour", nameof(newColour));

        Colour = newColour;
        AddDomainEvent(new TicketColourChangedEvent(this));
    }

    public void Delete()
    {
        AddDomainEvent(new TicketDeleted(this));
    }
}