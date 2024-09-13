using System.Reflection;
using Application.Common.Interfaces;
using Domain.TicketAggregate.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ReadOnlyApplicationDbContext : DbContext, IApplicationDbContext
{
    public ReadOnlyApplicationDbContext(DbContextOptions<ReadOnlyApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Ticket> Tickets => Set<Ticket>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}