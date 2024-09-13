using Domain.TicketAggregate.Entities;
using Domain.TicketAggregate.Enums;
using Domain.TicketAggregate.Events;
using Domain.TicketAggregate.Services;
using Domain.TicketAggregate.ValueObjects;

namespace Infrastructure.Services;

public class TicketService : ITicketService
{
    private readonly IUnitOfWork _unitOfWork;

    public TicketService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> CreateTicketAsync(string phoneNumber, Location location, CancellationToken cancellationToken)
    {
        var ticket = Ticket.Create(phoneNumber, location);
        await _unitOfWork.TicketRepository.AddAsync(ticket);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return ticket.Id;
    }

    public async Task DeleteTicketAsync(int id, CancellationToken cancellationToken)
    {
        var ticket = await _unitOfWork.TicketRepository.GetByIdAsync(id);
        if (ticket == null)
        {
            throw new KeyNotFoundException($"Ticket with id {id} not found.");
        }

        await _unitOfWork.TicketRepository.DeleteAsync(ticket);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task ChangeTicketColourAsync(int id, Colour colour, CancellationToken cancellationToken)
    {
        await _unitOfWork.TicketRepository.ChangeTicketColourAsync(id, colour, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task HandleTicketAsync(int id, CancellationToken cancellationToken)
    {
        await _unitOfWork.TicketRepository.HandleTicketAsync(id, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}