using Talabeyah.TicketManagement.Domain.Common;

namespace Talabeyah.TicketManagement.Application.Common.Interfaces;

public interface IEventDispatcher
{
    Task DispatchAsync(BaseEvent domainEvent);
}