using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Talabeyah.TicketManagement.Application.Common.Interfaces;

namespace Talabeyah.TicketManagement.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;
    private readonly IUser _user;
    private readonly IIdentityService _identityService;

    public LoggingBehaviour(ILogger<TRequest> logger, IUser user, IIdentityService identityService)
    {
        _logger = logger;
        _user = user;
        _identityService = identityService;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _user.Id ?? string.Empty;
        string? userName = string.Empty;

        if (!string.IsNullOrEmpty(userId))
        {
            //area of improvement by adding error handling in case of 
            // error in the fetching the user from Identity Service.
            try
            {
                userName = await _identityService.GetUserNameAsync(userId);

            }
            catch (Exception e)
            {
                _logger.logError(ex, "Error fetching user name for user ID {UserId}", userId)
            }
        }

        _logger.LogInformation("Ticket Management Request: {Name} {@UserId} {@UserName} {@Request}",
            requestName, userId, userName, request);
    }
}
