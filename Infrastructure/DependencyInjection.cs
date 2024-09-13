using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Talabeyah.TicketManagement.Application.Common.Repositories;
using Talabeyah.TicketManagement.Domain.Common;
using Talabeyah.TicketManagement.Infrastructure.Interceptors;
using Talabeyah.TicketManagement.Infrastructure.Repositories;

namespace Talabeyah.TicketManagement.Infrastructure;

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

        services.AddDbContext<ApplicationDbContextReadOnly>(options =>
        {
            var builder = new SqlConnectionStringBuilder(readConnectionString)
            {
                ApplicationIntent = ApplicationIntent.ReadOnly
            };
            options.UseSqlServer(builder.ConnectionString)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });


        services.AddScoped(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped(provider =>
            provider.GetRequiredService<ApplicationDbContextReadOnly>());


        services.AddScoped<ITicketRepository, TicketRepository>();
        return services;
    }
}