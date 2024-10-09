namespace DefaultNamespace;

public interface IPhoneNumberUniquenessChecker
{
    Task<bool> IsUniqueAsync(int id);
}