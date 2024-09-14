using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Talabeyah.TicketManagement.Domain.Entities;
using Talabeyah.TicketManagement.Infrastructure.Interceptors;

namespace Talabeyah.TicketManagement.Infrastructure;

//dbContext for Ticket Entity 
public class ApplicationDbContext : DbContext
{
    private readonly IMediator _mediator;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IMediator mediator) : base(options)
    {
        _mediator = mediator;
    }

    public DbSet<Ticket> Tickets => Set<Ticket>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        // optionsBuilder.UseInMemoryDatabase(databaseName: "TicketManagement");
        optionsBuilder.AddInterceptors(new DispatchDomainEventsInterceptor(_mediator));
    }
}