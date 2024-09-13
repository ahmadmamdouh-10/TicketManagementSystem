using Domain.TicketAggregate.Entities;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Ticket> Tickets { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

}