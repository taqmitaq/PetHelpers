using CSharpFunctionalExtensions;

namespace PetHelpers.Domain.Shared;

public class Description : ComparableValueObject
{
    private Description(string text) => Text = text;

    public string Text { get; }

    public static Result<Description, Error> Create(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return Errors.General.ValueIsRequired(nameof(Description));

        var description = new Description(text);

        return description;
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Text;
    }
}