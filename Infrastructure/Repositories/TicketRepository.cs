using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Talabeyah.TicketManagement.Application.Common.Mappings;
using Talabeyah.TicketManagement.Application.Common.Models;
using Talabeyah.TicketManagement.Application.Common.Repositories;
using Talabeyah.TicketManagement.Application.Tickets.Queries;
using Talabeyah.TicketManagement.Domain.Entities;
using Talabeyah.TicketManagement.Domain.Enums;

namespace Talabeyah.TicketManagement.Infrastructure.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly ApplicationDbContextReadOnly _applicationDbContextReadOnlyDbContext;
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;


    public TicketRepository(ApplicationDbContextReadOnly applicationDbContextReadOnlyDbContext,
        ApplicationDbContext dbContext,
        IMapper mapper)
    {
        _mapper = mapper;
        _applicationDbContextReadOnlyDbContext = applicationDbContextReadOnlyDbContext;
        _dbContext = dbContext;
    }
    
    public async Task<Ticket> GetByIdAsync(int id)
    {
        return await _dbContext.Tickets.FindAsync(new object[] { id });
    }

    public async Task<PaginatedList<TicketDto>> GetPageAsync(int pageNumber, int pageSize)
    {
        return await _dbContext.Tickets
            .OrderBy(x => x.Created)
            .ProjectTo<TicketDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(pageNumber, pageSize);
    }

    public async Task<int> AddAsync(Ticket ticket, CancellationToken cancellationToken)
    {
        await _dbContext.Tickets.AddAsync(ticket, cancellationToken);
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<int> UpdateAsync(Ticket ticket, CancellationToken cancellationToken)
    {
        _dbContext.Entry(ticket).State = EntityState.Modified;
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<int> DeleteAsync(Ticket ticket, CancellationToken cancellationToken)
    {
        _dbContext.Tickets.Remove(ticket);
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    //it should follow Single Responsibility Principle
    public async Task<int> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var ticket = await _dbContext.Tickets.FindAsync(id);
        if (ticket == null)
        {
            throw new KeyNotFoundException($"Ticket with id {id} not found.");
        }

        _dbContext.Tickets.Remove(ticket);
        ticket.Delete();
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}