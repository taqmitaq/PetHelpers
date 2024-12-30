using CSharpFunctionalExtensions;

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

    public static Result<FullName, string> Create(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return "First name cannot empty";

        if (string.IsNullOrWhiteSpace(lastName))
            return "Last name cannot empty";

        var fullName = new FullName(firstName, lastName);

        return Result.Success<FullName, string>(fullName);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
    }
}