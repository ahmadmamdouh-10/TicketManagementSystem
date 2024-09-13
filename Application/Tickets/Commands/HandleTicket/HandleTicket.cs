using Talabeyah.TicketManagement.Application.Common.Repositories;

namespace Talabeyah.TicketManagement.Application.Tickets.Commands.HandleTicket;

public record HandleTicket : IRequest
{
    public int Id { get; init; }

    public bool IsHandled { get; init; }
}

public class HandleTicketCommandHandler : IRequestHandler<HandleTicket>
{
    private readonly ITicketRepository _repository;

    public HandleTicketCommandHandler(ITicketRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(HandleTicket request, CancellationToken cancellationToken)
    {
        var ticket = await _repository.GetByIdAsync(request.Id);
        ticket.HandleTicket();
        await _repository.UpdateAsync(ticket, cancellationToken);
    }
}