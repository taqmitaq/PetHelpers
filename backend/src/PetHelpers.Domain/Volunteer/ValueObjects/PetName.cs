using CSharpFunctionalExtensions;

namespace PetHelpers.Domain.Volunteer.ValueObjects;

public class PetName : ValueObject
{
    private PetName(string value) => Value = value;

    public string Value { get; }

    public static Result<PetName, string> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return "Pet's name cannot be empty.";

        var petName = new PetName(name);

        return Result.Success<PetName, string>(petName);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}