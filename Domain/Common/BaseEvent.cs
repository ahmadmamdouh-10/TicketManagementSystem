using MediatR;

namespace Talabeyah.TicketManagement.Domain.Common;

public abstract class BaseEvent : INotification
{
    public DateTime OccuredOn { get; private set} = DateTime.UtcNow;
}