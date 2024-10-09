namespace DefaultNamespace;

public class PhoneNumberUniquenessChecker : IPhoneNumberUniquenessChecker
{
    private readonly ApplicationDbContext _context;
    
    public PhoneNumberUniquenessChecker(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public Task<bool> IsUniqueAsync(string phoneNumber)
    {
        return !await _context.Tickets.AnyAsync(u => u.PhoneNumber == phoneNumber);
    }
}