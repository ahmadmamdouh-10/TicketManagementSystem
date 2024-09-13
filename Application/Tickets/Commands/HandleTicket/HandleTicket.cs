using Application.Common.Interfaces;
using Domain.TicketAggregate.Events;
using Domain.TicketAggregate.Services;

namespace Application.Tickets.Commands.HandleTicket;

public record HandleTicket : IRequest
{
    public int Id { get; init; }

    //IsHandled property to update
    public bool IsHandled { get; init; }
}

public class HandleTicketCommandHandler : IRequestHandler<HandleTicket>
{
    private readonly ITicketService _ticketService;
    private readonly IValidator<HandleTicket> _validator;

    public HandleTicketCommandHandler(ITicketService ticketService, IValidator<HandleTicket> validator)
    {
        _ticketService = ticketService;
        _validator = validator;
    }

    public async Task Handle(HandleTicket request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        await _ticketService.HandleTicketAsync(request.Id, cancellationToken);
    }
}