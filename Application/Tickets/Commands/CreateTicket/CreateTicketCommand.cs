using Application.Common.Interfaces;
using Domain.TicketAggregate;
using Domain.TicketAggregate.Entities;
using Domain.TicketAggregate.Events;
using Domain.TicketAggregate.Services;
using Domain.TicketAggregate.ValueObjects;

namespace Application.Tickets.Commands.CreateTicket;

public record CreateTicketCommand : IRequest<int>
{
    public string PhoneNumber { get; set; }
    public Location Location { get; set; }
}

public class Handler : IRequestHandler<CreateTicketCommand, int>
{
    private readonly ITicketService _ticketService;
    private readonly IValidator<CreateTicketCommand> _validator;

    public Handler(ITicketService ticketService, IValidator<CreateTicketCommand> validator)
    {
        _ticketService = ticketService;
        _validator = validator;
    }

    public async Task<int> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        return await _ticketService.CreateTicketAsync(request.PhoneNumber, request.Location, cancellationToken);
    }
}