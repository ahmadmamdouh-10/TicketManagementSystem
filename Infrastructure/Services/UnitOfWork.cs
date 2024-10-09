namespace DefaultNamespace;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly IEventDispatcher _eventDispatcher;

    public UnitOfWork(ApplicationDbContext context, IEventDispatcher eventDispatcher)
    {
        _context = context;
        _eventDispatcher = eventDispatcher;
        Tickets = new TicketRepository(_context);
    }

    public ITicketRepository Tickets { get; }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await _context.SaveChangesAsync(cancellationToken);
        await DispatchDomainEventsAsync();
        return result;
    }

    private async Task DispatchDomainEventsAsync()
    {
        var domainEntities = _context.ChangeTracker
            .Entries<BaseEntity>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
        {
            await _eventDispatcher.Dispatch(domainEvent);
        }
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}