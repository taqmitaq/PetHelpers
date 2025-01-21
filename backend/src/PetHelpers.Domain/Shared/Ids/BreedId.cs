using CSharpFunctionalExtensions;

namespace PetHelpers.Domain.Shared.Ids;

public sealed class BreedId : ComparableValueObject
{
    private BreedId(Guid value) => Value = value;

    public Guid Value { get; }

    public static implicit operator Guid(BreedId value)
    {
        ArgumentNullException.ThrowIfNull(value);
        return value.Value;
    }

    public static implicit operator BreedId(Guid id)
    {
        return Create(id);
    }

    public static BreedId NewId() => new(Guid.NewGuid());

    public static BreedId Empty() => new(Guid.Empty);

    public static BreedId Create(Guid value) => new(value);

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}