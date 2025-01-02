using CSharpFunctionalExtensions;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Domain.Volunteer.ValueObjects;

public class FullName : ValueObject
{
    private FullName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public string FirstName { get; }

    public string LastName { get; }

    public static Result<FullName, Error> Create(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return Errors.General.ValueIsRequired(nameof(FirstName));

        if (string.IsNullOrWhiteSpace(lastName))
            return Errors.General.ValueIsRequired(nameof(LastName));

        var fullName = new FullName(firstName, lastName);

        return fullName;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
    }
}