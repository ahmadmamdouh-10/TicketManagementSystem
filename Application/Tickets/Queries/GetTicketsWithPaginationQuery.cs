using Talabeyah.TicketManagement.Application.Common.Models;
using Talabeyah.TicketManagement.Application.Common.Repositories;

namespace Talabeyah.TicketManagement.Application.Tickets.Queries;

public record GetTicketsWithPaginationQuery : IRequest<PaginatedList<TicketDto>>
{
    public int ListId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetTodoItemsWithPaginationQueryHandler : IRequestHandler<GetTicketsWithPaginationQuery,
    PaginatedList<TicketDto>>
{
    private readonly ITicketRepository _context;

    public GetTodoItemsWithPaginationQueryHandler(ITicketRepository context)
    {
        _context = context;
    }

    public async Task<PaginatedList<TicketDto>> Handle(GetTicketsWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.GetTicketsAsync(request.PageNumber, request.PageSize);
    }
}