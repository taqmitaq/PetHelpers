using CSharpFunctionalExtensions;

namespace PetHelpers.Domain.Shared.ValueObjects;

public class Title : ComparableValueObject
{
    private Title(string text) => Text = text;

    public string Text { get; }

    public static Result<Title, string> Create(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return "Title cannot be empty.";

        var title = new Title(text);

        return Result.Success<Title, string>(title);
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Text;
    }
}