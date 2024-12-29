using CSharpFunctionalExtensions;

namespace PetHelpers.Domain.Volunteer;

public class FullName : ValueObject
{
    private FullName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public string FirstName { get; }
    public string LastName { get; }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
    }
}