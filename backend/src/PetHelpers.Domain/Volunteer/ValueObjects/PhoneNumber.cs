using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace PetHelpers.Domain.Volunteer.ValueObjects;

public class PhoneNumber : ValueObject
{
    private PhoneNumber(string number) => Number = number;

    public string Number { get; }

    public static Result<PhoneNumber, string> Create(string number)
    {
        if (!Regex.Match(number, @"^(\+[0-9]{9})$").Success)
            return "Invalid phone number";

        var phoneNumber = new PhoneNumber(number);

        return Result.Success<PhoneNumber, string>(phoneNumber);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Number;
    }
}