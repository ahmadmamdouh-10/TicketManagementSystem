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
        services.AddScoped<IEventDispatcher, EventDispatcher>();
        services.AddScoped<IPhoneNumberUniquenessChecker, PhoneNumberUniquenessChecker>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
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
        
        //   //this needs you to install Microsoft.EntityFrameworkCore.Proxies
        //      options.UseLazyLoadingProxies(proxyOptions => 
        //  //this is will disable all the tracking of the entities, so you will need to manually change 
        //  // the changeModified state of the entity to modified before saving it:in .NET Core Api in Rest, why it's better to retrun Task<IActionResult> instead of returning the actual value result?   
        //     proxyOptions.UseChangeTrackingProxies(false))
        //     options.UseSqlServer(builder.ConnectionString)
        //         .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        // });

        #endregion
        
        return services;
    }
}