using CSharpFunctionalExtensions;

namespace PetHelpers.Domain.Volunteer;

public class PhoneNumber : ValueObject
{
    private PhoneNumber(string number) => Number = number;

    public string Number { get; private set;}
    
    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Number;
    }
}