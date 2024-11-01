using Talabeyah.TicketManagement.Application.Common.Repositories;

namespace Talabeyah.TicketManagement.Application.Common.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ITicketRepository Tickets { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}