using CSharpFunctionalExtensions;

namespace PetHelpers.Domain.Volunteer.ValueObjects;

public class Location : ValueObject
{
    private Location(string city, string region, string description, string postalCode)
    {
        City = city;
        Region = region;
        Description = description;
        PostalCode = postalCode;
    }

    public string City { get; }

    public string Region { get; }

    public string Description { get; }

    public string PostalCode { get; }

    public static Result<Location, string> Create(string city, string region, string description, string postalCode)
    {
        if (string.IsNullOrWhiteSpace(city))
            return "City is required.";

        if (string.IsNullOrWhiteSpace(region))
            return "Region is required.";

        if (string.IsNullOrWhiteSpace(description))
            return "Description is required.";

        if (string.IsNullOrWhiteSpace(postalCode))
            return "Postal code is required.";

        var location = new Location(city, region, description, postalCode);

        return Result.Success<Location, string>(location);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return City;
        yield return Region;
        yield return Description;
        yield return PostalCode;
    }
}