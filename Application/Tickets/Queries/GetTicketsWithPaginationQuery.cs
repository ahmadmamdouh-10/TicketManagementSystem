using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;

namespace Application.Tickets.Queries;

public record GetTicketsWithPaginationQuery : IRequest<PaginatedList<TicketDto>>
{
    public int ListId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetTodoItemsWithPaginationQueryHandler : IRequestHandler<GetTicketsWithPaginationQuery,
    PaginatedList<TicketDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTodoItemsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<TicketDto>> Handle(GetTicketsWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Tickets
            .OrderBy(x => x.Created)
            .ProjectTo<TicketDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}