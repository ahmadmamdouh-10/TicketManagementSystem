using System.Text.RegularExpressions;
using Domain.Common;

namespace Domain.TicketAggregate.ValueObjects;


public class PhoneNumber : ValueObject
{
    public string Value { get; }

    private PhoneNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Phone number cannot be empty", nameof(value));

        if (!IsValidPhoneNumber(value))
            throw new ArgumentException("Invalid phone number format", nameof(value));

        Value = value;
    }

    public static PhoneNumber Create(string value) => new PhoneNumber(value);

    private static bool IsValidPhoneNumber(string phoneNumber)
    {
        return Regex.IsMatch(phoneNumber, @"^\+?[1-9]\d{1,14}$");
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}