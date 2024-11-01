using MediatR;

namespace Talabeyah.TicketManagement.Domain.Common;

public abstract class BaseEvent : INotification
{
    private DateTime _occuredOn = DateTime.UtcNow;

    public DateTime OccuredOn
    {
        get => _occuredOn;
        private set => _occuredOn = value;
    }
}