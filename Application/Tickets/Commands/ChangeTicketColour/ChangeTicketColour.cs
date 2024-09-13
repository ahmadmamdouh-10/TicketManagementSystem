using Talabeyah.TicketManagement.Application.Common.Repositories;
using Talabeyah.TicketManagement.Domain.Enums;

namespace Talabeyah.TicketManagement.Application.Tickets.Commands.ChangeTicketColour;

public record ChangeTicketColour : IRequest
{
    public int Id { get; init; }
    public Color Colour { get; init; }
}

public class ChangeTicketColourHandler : IRequestHandler<ChangeTicketColour>
{
    private readonly ITicketRepository _repository;

    public ChangeTicketColourHandler(ITicketRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(ChangeTicketColour request, CancellationToken cancellationToken)
    {
        var ticket = await _repository.GetByIdAsync(request.Id);
        ticket.ChangeColour(request.Colour);
        await _repository.UpdateAsync(ticket, cancellationToken);
    }
}