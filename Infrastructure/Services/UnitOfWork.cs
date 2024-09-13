using Application.Common.Interfaces;
using Domain.TicketAggregate.Repositories;
using Domain.TicketAggregate.Services;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class UnitOfWork : IUnitOfWork
{
    private readonly IApplicationDbContext _context;

    private ITicketRepository _ticketRepository;

    public UnitOfWork(IApplicationDbContext context)
    {
        _context = context;
    }

    public ITicketRepository TicketRepository
    {
        get { return _ticketRepository ??= new TicketRepository(_context); }
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}