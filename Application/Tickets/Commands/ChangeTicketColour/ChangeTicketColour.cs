using Application.Common.Interfaces;
using Domain.TicketAggregate.Enums;
using Domain.TicketAggregate.Events;
using Domain.TicketAggregate.Services;

namespace Application.Tickets.Commands.ChangeTicketColour;

public record ChangeTicketColour : IRequest
{
    public int Id { get; init; }
    public Colour Colour { get; init; }
}

public class ChangeTicketColourHandler : IRequestHandler<ChangeTicketColour>
{
    private readonly ITicketService _ticketService;
    private readonly IValidator<ChangeTicketColour> _validator;

    public ChangeTicketColourHandler(ITicketService ticketService, IValidator<ChangeTicketColour> validator)
    {
        _ticketService = ticketService;
        _validator = validator;
    }

    public async Task Handle(ChangeTicketColour request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        await _ticketService.HandleTicketAsync(request.Id, cancellationToken);
    }
}