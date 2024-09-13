using Domain.TicketAggregate.Repositories;

namespace Domain.TicketAggregate.Services;

public interface IUnitOfWork
{
    ITicketRepository TicketRepository { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}