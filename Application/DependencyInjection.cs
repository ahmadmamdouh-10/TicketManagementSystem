using System.Reflection;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Talabeyah.TicketManagement.Application.Common.Behaviours;
using Talabeyah.TicketManagement.Application.Common.Interfaces;
using Talabeyah.TicketManagement.Application.Common.Services;

namespace Talabeyah.TicketManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services
        , IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        });

        services.AddScoped<IChangeTicketColor, ChangeTicketColorService>();


        // Add Hangfire services

        #region AddingHangfireWithSqlServer

        // var connectionString = configuration.GetConnectionString("HangfireConnection");
        // services.AddHangfire(config => config.UseSqlServerStorage(connectionString));

        #endregion

        services.AddHangfire(config => config.UseMemoryStorage());
        services.AddHangfireServer();

        return services;
    }
}