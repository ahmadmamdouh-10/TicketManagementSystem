using JetBrains.Annotations;
using Talabeyah.TicketManagement.Application.Common.Repositories;
using Talabeyah.TicketManagement.Domain.Entities;
using Talabeyah.TicketManagement.Domain.ValueObjects;

namespace Talabeyah.TicketManagement.Application.Tickets.Commands.CreateTicket;

public record CreateTicketCommand : IRequest<int>
{
    public string PhoneNumber { get; set; }
    public Location Location { get; set; }
}

[UsedImplicitly]
public class Handler : IRequestHandler<CreateTicketCommand, int>
{
    private readonly ITicketRepository _repository;
    private readonly IPhoneNumberUniquenessChecker _phoneNumberUniquenessChecker;
    
    private readonly IUnitOfWork _unitOfWork;


    public Handler(ITicketRepository ticketRepository, 
        IPhoneNumberUniquenessChecker phoneNumberUniquenessChecker,
        IUnitOfWork unitOfWork)
    {
        _repository = ticketRepository;
        _phoneNumberUniquenessChecker = phoneNumberUniquenessChecker;
        _unitOfWork = unitOfWork;
    }
    public async Task<int> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
    {
        if (!await _phoneNumberUniquenessChecker.IsUniqueAsync(request.PhoneNumber, cancellationToken))
            throw new BadRequestException("Phone number is already taken");
        
        var ticket = Ticket.Create(request.PhoneNumber, request.Location);
        await _repository.AddAsync(ticket, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return ticket.Id;
    }
}