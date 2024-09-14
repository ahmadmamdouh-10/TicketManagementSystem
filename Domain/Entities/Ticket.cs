using Talabeyah.TicketManagement.Domain.Common;
using Talabeyah.TicketManagement.Domain.Enums;
using Talabeyah.TicketManagement.Domain.Events;
using Talabeyah.TicketManagement.Domain.ValueObjects;

namespace Talabeyah.TicketManagement.Domain.Entities;

public class Ticket : BaseAuditableEntity, IAgreggateRoot
{
    public PhoneNumber PhoneNumber { get; private set; }
    public Location Location { get; private set; }
    public bool IsHandled { get; private set; }
    public Color Colour { get; private set; }

    // Private constructor to enforce the use of the factory method
    private Ticket()
    {
        Colour = Color.None;
        IsHandled = false;
    }
    public void HandleTicket()
    {
        if (IsHandled)
            throw new InvalidOperationException("Ticket is already handled");

        IsHandled = true;
        AddDomainEvent(new TicketHandledEvent(this));
    }

    public void ChangeColour()
    {
        Color nextColor = Colour switch
        {
            Color.None => Color.Yellow,
            Color.Yellow => Color.Green,
            Color.Green => Color.Blue,
            Color.Blue => Color.Red,
            _ => Color.None
        };
        if (nextColor == Color.None)
            throw new ArgumentException("Invalid colour", nameof(Colour));

        Colour = nextColor;
        // AddDomainEvent(new TicketColourChangedEvent(this));
    }

    public void Delete()
    {
        AddDomainEvent(new TicketDeletedEvent(this));
    }

    // Static factory method to create a Ticket and add the domain event
    public static Ticket Create(string phoneNumber, Location location)
    {
        var ticket = new Ticket
        {
            PhoneNumber = PhoneNumber.Create(phoneNumber),
            Location = location
        };
        ticket.AddDomainEvent(new TicketCreatedEvent(ticket));
        return ticket;
    }
}