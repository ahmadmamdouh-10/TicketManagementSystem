using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Talabeyah.TicketManagement.Domain.Entities;

namespace Talabeyah.TicketManagement.Infrastructure;

//dbContext for Ticket Entity 
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Ticket> Tickets => Set<Ticket>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}