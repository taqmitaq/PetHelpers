using CSharpFunctionalExtensions;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Domain.Volunteer.ValueObjects;

public class PetName : ValueObject
{
    private PetName(string value) => Value = value;

    public string Value { get; }

    public static Result<PetName, Error> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Errors.General.ValueIsRequired("PetName");

        var petName = new PetName(name);

        return petName;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}