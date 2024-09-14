# Ticket Management System
## Overview
The Ticket Management System is a robust application designed to manage tickets efficiently. It follows best practices of Domain-Driven Design (DDD), Command Query Responsibility Segregation (CQRS), and Clean Architecture. The solution leverages MediatR for handling commands and queries, and Hangfire for background job processing.
## Features
- **Domain-Driven Design (DDD)**: Ensures the domain model is the core of the application.
- **CQRS**: Separates read and write operations to optimize performance and scalability.
- **MediatR**: Simplifies the implementation of the mediator pattern for handling commands and queries.
- **FluentValidation**: Provides a fluent interface for validating commands and queries.
- **Hangfire**: Manages background jobs and scheduled tasks.
- **Entity Framework Core**: Facilitates database interactions with separate DbContexts for read and write operations.
- **In-Memory Database**: Supports in-memory database for testing purposes.
## Project Structure
- **Domain**: Contains the core business logic, including entities, value objects, and domain events.
- **Application**: Contains application logic, including commands, queries, validators, and handlers.
- **Infrastructure**: Contains data access logic, including DbContexts, repositories, and services.
- **Web**: Contains the API controllers and configuration for the web application.
## Configuration
The application uses two separate DbContexts for read and write operations. The connection strings are defined in the `appsettings.json` file:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=TicketManagementDb;Username=yourusername;Password=yourpassword",
    "ReadOnlyConnection": "Host=localhost;Database=TicketManagementDb;Username=yourusername;Password=yourpassword;ApplicationIntent=ReadOnly",
    "HangfireConnection": "Host=localhost;Database=HangfireDb;Username=yourusername;Password=yourpassword",
    "myTestDb": "TicketManagement"
  }
}
```
## In-Memory Database for Testing
For testing purposes, the application can be configured to use an in-memory database. This is useful for running tests without the need for a real database. The in-memory database is configured in the `AddInfrastructureServices` method in `DependencyInjection.cs`:
```csharp
public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
{
    services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
    services.AddScoped<ITicketRepository, TicketRepository>();
    var myTestDb = configuration.GetConnectionString("myTestDb");
    Guard.AgainstNull(myTestDb, "Connection string 'myTestDb' not found.");
    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseInMemoryDatabase(myTestDb!)
    );
    services.AddDbContext<ApplicationDbContextReadOnly>(options => options
        .UseInMemoryDatabase(myTestDb!)
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
    );
    return services;
}
```
## Reference
This project was inspired by the Clean Architecture template by Jason Taylor. You can find the template repository [here](https://github.com/jasontaylordev/CleanArchitecture).
## Getting Started
To get started with the Ticket Management System, follow these steps:
1. Clone the repository.
2. Configure the connection strings in `appsettings.json`.
3. Run the application.
## License
This project is licensed under the MIT License.
