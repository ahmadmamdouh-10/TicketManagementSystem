using Domain.Common;
using Domain.TicketAggregate.Entities;
using Domain.TicketAggregate.Enums;

namespace Domain.TicketAggregate.Repositories;

public interface ITicketRepository : IRepository<Ticket>
{
    Task<Ticket> GetByIdAsync(int id);
    Task AddAsync(Ticket ticket);
    Task DeleteAsync(Ticket ticket);
    Task<Ticket> ChangeTicketColourAsync(int id, Colour colour, CancellationToken cancellationToken);
    Task<Ticket> HandleTicketAsync(int id, CancellationToken cancellationToken);
}