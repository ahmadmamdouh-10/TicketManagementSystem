using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Talabeyah.TicketManagement.Application.Common.Interfaces;
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
        // services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
        services.AddScoped<ITicketRepository, TicketRepository>();

        var myTestDb = configuration.GetConnectionString("myTestDb");

        Guard.AgainstNull(myTestDb, "Connection string 'myTestDb' not found.");

        #region addingInMemoryDatabase

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase(myTestDb!)
        );
        services.AddDbContext<ApplicationDbContextReadOnly>(options => options
            .UseInMemoryDatabase(myTestDb!)
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
        );

        #endregion
        
        #region addingSqlServerDatabase

        // services.AddDbContext<ApplicationDbContext>(options =>
        //     options.UseSqlServer(writeConnectionString));
        //
        // services.AddDbContext<ApplicationDbContextReadOnly>(options =>
        // {
        //     var builder = new SqlConnectionStringBuilder(readConnectionString)
        //     {
        //         ApplicationIntent = ApplicationIntent.ReadOnly
        //     };
        //     options.UseSqlServer(builder.ConnectionString)
        //         .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        // });

        #endregion
        
        return services;
    }
}