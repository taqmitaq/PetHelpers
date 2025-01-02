using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Domain.Volunteer.ValueObjects;

public class PhoneNumber : ValueObject
{
    private PhoneNumber(string number) => Number = number;

    public string Number { get; }

    public static Result<PhoneNumber, Error> Create(string number)
    {
        if (!Regex.Match(number, @"^(\+[0-9]{9})$").Success)
            return Errors.General.ValueIsInvalid(nameof(PhoneNumber));

        var phoneNumber = new PhoneNumber(number);

        return phoneNumber;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Number;
    }
}