namespace Domain.Common;

public static class Guard
{
    public static void AgainstNull(object value, string parameterName)
    {
        if (value == null)
            throw new ArgumentNullException(parameterName);
    }

    public static void AgainstInvalidAmount(decimal amount, string parameterName)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be greater than zero", parameterName);
    }
}