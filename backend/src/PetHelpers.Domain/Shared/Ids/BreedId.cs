using CSharpFunctionalExtensions;

namespace PetHelpers.Domain.Shared.Ids;

public class BreedId : ComparableValueObject
{
    public BreedId(Guid value) => Value = value;
    
    public Guid Value { get; }
    
    public static BreedId NewBreedId() => new(Guid.NewGuid());
    public static BreedId Empty() => new(Guid.Empty);

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}