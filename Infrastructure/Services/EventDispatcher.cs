namespace DefaultNamespace;

public class EventDispatcher : IEventDispatcher
{
    private readonly IMediator _mediator;
    
    public EventDispatcher(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task DispatchAsync(BaseEvent domainEvent)
    {
        await _mediator.Publish(domainEvent);
    }
}