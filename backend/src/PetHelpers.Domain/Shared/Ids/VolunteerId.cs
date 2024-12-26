using CSharpFunctionalExtensions;

namespace PetHelpers.Domain.Shared.Ids;

public class VolunteerId : ComparableValueObject
{
    public VolunteerId(Guid value) => Value = value;
    
    public Guid Value { get; }
    
    public static VolunteerId NewVolunteerId() => new(Guid.NewGuid());
    public static VolunteerId Empty() => new(Guid.Empty);
    
    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}