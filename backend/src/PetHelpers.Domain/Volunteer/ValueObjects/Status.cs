using CSharpFunctionalExtensions;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Domain.Volunteer.ValueObjects;

public class Status : ValueObject
{
    public static readonly Status NeedsHelp = new(nameof(NeedsHelp));
    public static readonly Status LookingForHome = new(nameof(LookingForHome));
    public static readonly Status FoundHome = new(nameof(FoundHome));

    private static readonly Status[] _all = [NeedsHelp, LookingForHome, FoundHome];

    private Status(string value) => Value = value;

    public string Value { get; }

    public static Result<Status, Error> Create(string value)
    {
        value = value.Trim();

        if (_all.Any(s => string.Equals(value, s.Value, StringComparison.InvariantCultureIgnoreCase)))
        {
            var status = new Status(value);

            return status;
        }

        return Errors.General.ValueIsInvalid("Status");
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}