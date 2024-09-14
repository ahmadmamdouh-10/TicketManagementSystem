using Talabeyah.TicketManagement.Application.Common.Models;
using Talabeyah.TicketManagement.Application.Tickets.Queries;
using Talabeyah.TicketManagement.Domain.Common;
using Talabeyah.TicketManagement.Domain.Entities;
using Talabeyah.TicketManagement.Domain.Enums;

namespace Talabeyah.TicketManagement.Application.Common.Repositories;

public interface ITicketRepository : IRepository<Ticket>
{
    Task<Ticket> GetByIdAsync(int id);
    Task<PaginatedList<TicketDto>> GetPageAsync(int pageNumber, int pageSize);
    Task<int> AddAsync(Ticket ticket, CancellationToken cancellationToken);
    Task<int> UpdateAsync(Ticket ticket, CancellationToken cancellationToken);
    Task<int> DeleteAsync(Ticket ticket, CancellationToken cancellationToken);
    // Task<Ticket> ChangeTicketColourAsync(int id, Color color, CancellationToken cancellationToken);
    // Task<Ticket> HandleTicketAsync(int id, CancellationToken cancellationToken);
}