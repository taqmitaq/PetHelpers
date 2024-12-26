using CSharpFunctionalExtensions;

namespace PetHelpers.Domain.Shared.Ids;

public class PetId : ComparableValueObject
{
    public PetId(Guid value) => Value = value;
    
    public Guid Value { get; }
    
    public static PetId NewPetId() => new(Guid.NewGuid());
    public static PetId Empty() => new(Guid.Empty);

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}