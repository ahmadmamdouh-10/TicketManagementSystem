using AutoMapper;
using Talabeyah.TicketManagement.Application.Common.Interfaces;
using Talabeyah.TicketManagement.Application.Common.Repositories;
using Talabeyah.TicketManagement.Domain.Common;
using Talabeyah.TicketManagement.Infrastructure.Repositories;

namespace Talabeyah.TicketManagement.Infrastructure.Services;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IEventDispatcher _eventDispatcher;
    private readonly IMapper _mapper;

    public UnitOfWork(ApplicationDbContext context,
        IEventDispatcher eventDispatcher,
        IMapper mapper
        )
    {
        _dbContext = context;
        _eventDispatcher = eventDispatcher;
        _mapper = mapper;
        Tickets = new TicketRepository(_dbContext, _mapper);
    }

    public ITicketRepository Tickets { get; }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
        await DispatchDomainEventsAsync();
        return result;
    }

    private async Task DispatchDomainEventsAsync()
    {
        var domainEntities = _dbContext.ChangeTracker
            .Entries<BaseEntity>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

        var entityEntries = domainEntities.ToList();
        var domainEvents = entityEntries
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        entityEntries.ToList()
            .ForEach(entity => entity.Entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
        {
            await _eventDispatcher.DispatchAsync(domainEvent);
        }
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}