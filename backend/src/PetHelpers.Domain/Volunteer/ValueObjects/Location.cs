using CSharpFunctionalExtensions;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Domain.Volunteer.ValueObjects;

public class Location : ValueObject
{
    private Location(string city, string region, string postalCode)
    {
        City = city;
        Region = region;
        PostalCode = postalCode;
    }

    public string City { get; }

    public string Region { get; }

    public string PostalCode { get; }

    public static Result<Location, Error> Create(string city, string region, string postalCode)
    {
        if (string.IsNullOrWhiteSpace(city))
            return Errors.General.ValueIsRequired(nameof(City));

        if (string.IsNullOrWhiteSpace(region))
            return Errors.General.ValueIsRequired(nameof(Region));

        if (string.IsNullOrWhiteSpace(postalCode))
            return Errors.General.ValueIsRequired(nameof(PostalCode));

        var location = new Location(city, region, postalCode);

        return location;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return City;
        yield return Region;
        yield return PostalCode;
    }
}