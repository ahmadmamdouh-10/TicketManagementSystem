using Talabeyah.TicketManagement.Domain.Common;

namespace Talabeyah.TicketManagement.Domain.ValueObjects;

public class Location : ValueObject
{
    public string Governorate { get; }
    public string City { get; }
    public string District { get; }

    public Location(string governorate, string city, string district)
    {
        Governorate = governorate;
        City = city;
        District = district;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Governorate;
        yield return City;
        yield return District;
    }
}