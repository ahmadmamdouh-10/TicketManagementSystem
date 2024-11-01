using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Talabeyah.TicketManagement.Application.Common.Interfaces;

namespace Talabeyah.TicketManagement.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest>(ILogger<TRequest> logger, IUser user, IIdentityService identityService)
    : IRequestPreProcessor<TRequest>
    where TRequest : notnull
{
    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = user.Id ?? string.Empty;
        string? userName = string.Empty;

        if (!string.IsNullOrEmpty(userId))
        {
            //area of improvement by adding error handling in case of 
            // error in the fetching the user from Identity Service.
            try
            {
                userName = await identityService.GetUserNameAsync(userId);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error fetching user name for user ID {UserId}", userId);
            }
        }

        logger.LogInformation("Ticket Management Request: {Name} {@UserId} {@UserName} {@Request}",
            requestName, userId, userName, request);
    }
}
