using Application.Common.Interfaces;
using Domain.Common;
using Domain.TicketAggregate.Repositories;
using Domain.TicketAggregate.Services;
using Infrastructure.Interceptors;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        var writeConnectionString = configuration.GetConnectionString("DefaultConnection");
        Guard.AgainstNull(writeConnectionString, "Connection string 'DefaultConnection' not found.");

        var readConnectionString = configuration.GetConnectionString("ReadOnlyConnection");
        Guard.AgainstNull(readConnectionString, "Connection string 'ReadOnlyConnection' not found.");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(writeConnectionString));

        services.AddDbContext<ReadOnlyApplicationDbContext>(options =>
        {
            var builder = new SqlConnectionStringBuilder(readConnectionString)
            {
                ApplicationIntent = ApplicationIntent.ReadOnly
            };
            options.UseSqlServer(builder.ConnectionString);
        });
        
        
        
        

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ReadOnlyApplicationDbContext>());


        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITicketService, TicketService>();

        return services;
    }
}