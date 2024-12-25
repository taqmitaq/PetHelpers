using CSharpFunctionalExtensions;

namespace PetHelpers.Domain.Volunteer;

public class Status : ValueObject
{
    public static readonly Status NeedsHelp = new(nameof(NeedsHelp));
    public static readonly Status LookingForHome = new(nameof(LookingForHome));
    public static readonly Status FoundHome = new(nameof(FoundHome));
    
    private static readonly Status[] _all = [NeedsHelp, LookingForHome, FoundHome];

    private Status(string value) => Value = value;

    public string Value { get; private set; }
    
    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}