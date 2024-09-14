using Ardalis.GuardClauses;
using Talabeyah.TicketManagement.Application.Common.Repositories;
using Talabeyah.TicketManagement.Domain.Entities;

namespace Talabeyah.TicketManagement.Application.Tickets.Commands.DeleteTicket;

public record DeleteTicketCommand(int Id) : IRequest;

[UsedImplicitly]
public class DeleteTicketCommandHandler : IRequestHandler<DeleteTicketCommand>
{
    private readonly ITicketRepository _repository;


    public DeleteTicketCommandHandler(ITicketRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
    {
        
        var ticket = await _repository.GetByIdAsync(request.Id);
        if (ticket == null)
        {
            throw new NotFoundException(nameof(Ticket), request.Id.ToString());
        }
        ticket.Delete();
        await _repository.DeleteAsync(ticket, cancellationToken);
    }
}