using CSharpFunctionalExtensions;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Domain.Volunteer.ValueObjects;

public class Position : ComparableValueObject
{
    public static Position First = new(1);

    private Position(int value)
    {
        Value = value;
    }

    public int Value { get; }

    public static Result<Position, Error> Create(int position)
    {
        if (position <= 0)
        {
            return Errors.General.ValueIsInvalid("position");
        }

        return new Position(position);
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}