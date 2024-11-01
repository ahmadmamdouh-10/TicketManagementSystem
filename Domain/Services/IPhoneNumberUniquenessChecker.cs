namespace Talabeyah.TicketManagement.Domain.Services;

public interface IPhoneNumberUniquenessChecker
{
    Task<bool> IsUniqueAsync(string id);
}