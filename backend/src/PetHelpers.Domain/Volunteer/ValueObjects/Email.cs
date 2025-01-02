using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Domain.Volunteer.ValueObjects;

public class Email : ValueObject
{
    private const string VALID_EMAIL_PATTERN =
        """
        ^(?!\.)("([^"\r\\]|\\["\r\\])*"|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?
        <!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$
        """;

    public string Address { get; }

    private Email(string address)
    {
        Address = address;
    }

    public static Result<Email, Error> Create(string address)
    {
        address = address.Trim();

        if (!Regex.Match(address, VALID_EMAIL_PATTERN, RegexOptions.IgnoreCase).Success)
            return Errors.General.ValueIsInvalid(nameof(Email));

        var email = new Email(address);

        return email;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Address;
    }
}