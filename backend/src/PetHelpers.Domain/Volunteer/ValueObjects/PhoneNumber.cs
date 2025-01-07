using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Domain.Volunteer.ValueObjects;

public class PhoneNumber : ValueObject
{
    private const string VALID_PHONE_PATTERN = @"^([0-9]{11})$";

    private PhoneNumber(string number) => Number = number;

    public string Number { get; }

    public static Result<PhoneNumber, Error> Create(string number)
    {
        string phone = Regex.Replace(number, @"\D", String.Empty);

        if (Regex.Match(phone, VALID_PHONE_PATTERN).Success == false)
            return Errors.General.ValueIsInvalid("PhoneNumber");

        var phoneNumber = new PhoneNumber(phone);

        return phoneNumber;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Number;
    }
}