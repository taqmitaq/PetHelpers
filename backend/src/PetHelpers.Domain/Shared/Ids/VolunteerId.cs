﻿using CSharpFunctionalExtensions;

namespace PetHelpers.Domain.Shared.Ids;

public sealed class VolunteerId : ComparableValueObject
{
    private VolunteerId(Guid value) => Value = value;

    public Guid Value { get; }

    public static implicit operator Guid(VolunteerId value)
    {
        ArgumentNullException.ThrowIfNull(value);
        return value.Value;
    }

    public static implicit operator VolunteerId(Guid id)
    {
        return Create(id);
    }

    public static VolunteerId NewId() => new(Guid.NewGuid());

    public static VolunteerId Empty() => new(Guid.Empty);

    public static VolunteerId Create(Guid value) => new(value);

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}