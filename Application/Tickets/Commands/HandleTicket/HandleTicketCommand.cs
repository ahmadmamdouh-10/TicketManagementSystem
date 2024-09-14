using Ardalis.GuardClauses;
using JetBrains.Annotations;
using Talabeyah.TicketManagement.Application.Common.Repositories;

namespace Talabeyah.TicketManagement.Application.Tickets.Commands.HandleTicket;

public record HandleTicketCommand : IRequest
{
    public int Id { get; init; }

    public bool IsHandled { get; init; }
}

[UsedImplicitly]
public class HandleTicketCommandHandler : IRequestHandler<HandleTicketCommand>
{
    private readonly ITicketRepository _repository;

    public HandleTicketCommandHandler(ITicketRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(HandleTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _repository.GetByIdAsync(request.Id);
        if (ticket is null)
        {
            throw new NotFoundException(request.Id.ToString(), nameof(ticket));
        }

        ticket.HandleTicket();
        await _repository.UpdateAsync(ticket, cancellationToken);
    }
}