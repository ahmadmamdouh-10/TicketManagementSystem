using Application.Common.Interfaces;
using Domain.TicketAggregate.Events;

namespace Application.Tickets.Commands.HandleTicket;

public record HandleTicket : IRequest
{
    public int Id { get; init; }

    //IsHandled property to update
    public bool IsHandled { get; init; }
}

public class HandleTicketCommandHandler : IRequestHandler<HandleTicket>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<HandleTicket> _validator;

    public HandleTicketCommandHandler(IApplicationDbContext context , IValidator<HandleTicket> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task Handle(HandleTicket request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var entity = await _context.Tickets
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.HandleTicket();

        entity.AddDomainEvent(new TicketHandledEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}