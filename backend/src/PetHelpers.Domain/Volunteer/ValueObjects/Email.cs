using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

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

    public static Result<Email, string> Create(string address)
    {
        address = address.Trim();

        if (!Regex.Match(address, VALID_EMAIL_PATTERN, RegexOptions.IgnoreCase).Success)
            return "Invalid email address";

        var email = new Email(address);

        return Result.Success<Email, string>(email);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Address;
    }
}