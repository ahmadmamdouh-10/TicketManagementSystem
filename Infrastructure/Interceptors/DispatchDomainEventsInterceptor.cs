using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Talabeyah.TicketManagement.Domain.Common;

namespace Talabeyah.TicketManagement.Infrastructure.Interceptors;

public class DispatchDomainEventsInterceptor : SaveChangesInterceptor
{
    private readonly IMediator _mediator;

    public DispatchDomainEventsInterceptor(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        var eveInterceptionResult = base.SavingChanges(eventData, result);

        DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();
        return eveInterceptionResult;
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        var eveInterceptionResult = await base.SavingChangesAsync(eventData, result, cancellationToken);
        await DispatchDomainEvents(eventData.Context);
        return eveInterceptionResult;
    }

    public async Task DispatchDomainEvents(DbContext context)
    {
        if (context == null) return;

        var entities = context.ChangeTracker
            .Entries<BaseEntity>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity);

        var domainEvents = entities
            .SelectMany(e => e.DomainEvents)
            .ToList();

        entities.ToList().ForEach(e => e.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await _mediator.Publish(domainEvent);
    }
}