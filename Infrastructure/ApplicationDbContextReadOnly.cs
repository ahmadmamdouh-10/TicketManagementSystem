using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Talabeyah.TicketManagement.Domain.Entities;

namespace Talabeyah.TicketManagement.Infrastructure;

public class ApplicationDbContextReadOnly : DbContext
{
    public ApplicationDbContextReadOnly(DbContextOptions<ApplicationDbContextReadOnly> options) : base(options)
    {
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
    }
}