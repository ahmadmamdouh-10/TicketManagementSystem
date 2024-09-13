using Application.Common.Interfaces;
using Domain.TicketAggregate.Entities;
using Domain.TicketAggregate.Enums;
using Domain.TicketAggregate.Events;
using Domain.TicketAggregate.Repositories;

namespace Infrastructure.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly IApplicationDbContext _context;

    public TicketRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Ticket> GetByIdAsync(int id)
    {
        return await _context.Tickets.FindAsync(id);
    }

    public async Task AddAsync(Ticket ticket)
    {
        await _context.Tickets.AddAsync(ticket);
    }

    public Task DeleteAsync(Ticket ticket)
    {
        _context.Tickets.Remove(ticket);
        ticket.Delete();
        return Task.CompletedTask;
    }

    public async Task<Ticket> ChangeTicketColourAsync(int id, Colour colour, CancellationToken cancellationToken)
    {
        var ticket = await _context.Tickets.FindAsync(new object[] { id }, cancellationToken);
        if (ticket == null)
        {
            throw new KeyNotFoundException($"Ticket with id {id} not found.");
        }

        ticket.ChangeColour(colour);
        return ticket;
    }

    public async Task<Ticket> HandleTicketAsync(int id, CancellationToken cancellationToken)
    {
        var ticket = await _context.Tickets.FindAsync(new object[] { id }, cancellationToken);
        if (ticket == null)
        {
            throw new KeyNotFoundException($"Ticket with id {id} not found.");
        }

        if (!ticket.IsHandled)
        {
            ticket.HandleTicket();
        }

        return ticket;
    }
}