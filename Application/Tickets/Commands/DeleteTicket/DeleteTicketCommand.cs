using Application.Common.Interfaces;
using Domain.TicketAggregate.Events;
using Domain.TicketAggregate.Services;

namespace Application.Tickets.Commands.DeleteTicket;

public record DeleteTicketCommand(int Id) : IRequest;

public class DeleteTicketCommandHandler : IRequestHandler<DeleteTicketCommand>
{
    private readonly ITicketService _ticketService;
    private readonly IValidator<DeleteTicketCommand> _validator;


    public DeleteTicketCommandHandler(ITicketService ticketService, IValidator<DeleteTicketCommand> validator)
    { 
        _ticketService = ticketService;
        _validator = validator;
    }

    public async Task Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        await _ticketService.DeleteTicketAsync(request.Id, cancellationToken);
    }
}