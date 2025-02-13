using CSharpFunctionalExtensions;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Domain.Volunteer.ValueObjects;

public class SerialNumber : ComparableValueObject
{
    public static SerialNumber First = new(1);

    private SerialNumber(int value)
    {
        Value = value;
    }

    public int Value { get; }

    public static Result<SerialNumber, Error> Create(int number)
    {
        if (number <= 0)
        {
            return Errors.General.ValueIsInvalid("serial number");
        }

        return new SerialNumber(number);
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}