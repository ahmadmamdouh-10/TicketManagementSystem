using System.Text.RegularExpressions;
using Talabeyah.TicketManagement.Domain.Common;

namespace Talabeyah.TicketManagement.Domain.ValueObjects;


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


    private static bool IsValidPhoneNumber(string phoneNumber)
    {
        return Regex.IsMatch(phoneNumber, @"^\+?[1-9]\d{1,14}$");
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    
        public static PhoneNumber Create(string value) => new PhoneNumber(value);

}