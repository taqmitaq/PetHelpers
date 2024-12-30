using CSharpFunctionalExtensions;

namespace PetHelpers.Domain.Shared.ValueObjects;

public class Description : ComparableValueObject
{
    private Description(string text) => Text = text;

    public string Text { get; }

    public static Result<Description, string> Create(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return "Description cannot be empty.";

        var description = new Description(text);

        return Result.Success<Description, string>(description);
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Text;
    }
}