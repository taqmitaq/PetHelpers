using CSharpFunctionalExtensions;

namespace PetHelpers.Domain.Shared.Ids;

public sealed class PetId : ComparableValueObject
{
    private PetId(Guid value) => Value = value;
    
    public Guid Value { get; }
    
    public static PetId NewPetId() => new(Guid.NewGuid());
    public static PetId Empty() => new(Guid.Empty);
    public static PetId Create(Guid value) => new(value);

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}