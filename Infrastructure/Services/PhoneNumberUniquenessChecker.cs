using System.Data.Entity;
using Talabeyah.TicketManagement.Domain.Services;

namespace Talabeyah.TicketManagement.Infrastructure.Services;

public class PhoneNumberUniquenessChecker : IPhoneNumberUniquenessChecker
{
    private readonly ApplicationDbContext _context;
    
    public PhoneNumberUniquenessChecker(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> IsUniqueAsync(string phoneNumber)
    {
        return !await _context.Tickets.AnyAsync(u => u.PhoneNumber.Equals(phoneNumber));
    }
}