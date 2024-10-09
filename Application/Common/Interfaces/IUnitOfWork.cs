namespace DefaultNamespace;

public class IUnitOfWork : IDisposable
{
    ITicketRepository Tickets { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}