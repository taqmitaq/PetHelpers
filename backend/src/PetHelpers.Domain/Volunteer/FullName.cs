using CSharpFunctionalExtensions;

namespace PetHelpers.Domain.Volunteer;

public class FullName : ValueObject
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    private FullName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
    
    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
    }
}