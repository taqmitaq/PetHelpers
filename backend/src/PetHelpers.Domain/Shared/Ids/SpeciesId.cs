using CSharpFunctionalExtensions;

namespace PetHelpers.Domain.Shared.Ids;

public class SpeciesId : ComparableValueObject
{
    public SpeciesId(Guid value) => Value = value;
    
    public Guid Value { get; }
    
    public static SpeciesId NewSpeciesId() => new(Guid.NewGuid());
    public static SpeciesId Empty() => new(Guid.Empty);
    
    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}