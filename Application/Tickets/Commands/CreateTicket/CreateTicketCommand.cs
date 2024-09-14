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

    public Handler(ITicketRepository ticketRepository)
    {
        _repository = ticketRepository;
    }

    public async Task<int> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = Ticket.Create(request.PhoneNumber, request.Location);
        await _repository.AddAsync(ticket, cancellationToken);
        return ticket.Id;
    }
}