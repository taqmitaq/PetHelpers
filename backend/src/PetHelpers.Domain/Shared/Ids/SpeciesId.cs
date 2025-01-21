using CSharpFunctionalExtensions;

namespace PetHelpers.Domain.Shared.Ids;

public sealed class SpeciesId : ComparableValueObject
{
    private SpeciesId(Guid value) => Value = value;

    public Guid Value { get; }

    public static implicit operator Guid(SpeciesId value)
    {
        ArgumentNullException.ThrowIfNull(value);
        return value.Value;
    }

    public static implicit operator SpeciesId(Guid id)
    {
        return Create(id);
    }

    public static SpeciesId NewId() => new(Guid.NewGuid());

    public static SpeciesId Empty() => new(Guid.Empty);

    public static SpeciesId Create(Guid value) => new(value);

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}