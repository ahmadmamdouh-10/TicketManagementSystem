namespace DefaultNamespace;

public interface IEventDispatcher
{
    Task DispatchAsync(BaseEvent domainEvent);
}