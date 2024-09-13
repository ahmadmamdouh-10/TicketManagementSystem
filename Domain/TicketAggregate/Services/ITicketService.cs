using Domain.TicketAggregate.Enums;
using Domain.TicketAggregate.ValueObjects;

namespace Domain.TicketAggregate.Services;

public interface ITicketService
{
    Task<int> CreateTicketAsync(string phoneNumber, Location location, CancellationToken cancellationToken);
    Task DeleteTicketAsync(int id, CancellationToken cancellationToken);
    Task ChangeTicketColourAsync(int id, Colour colour, CancellationToken cancellationToken);
    Task HandleTicketAsync(int id, CancellationToken cancellationToken);
}