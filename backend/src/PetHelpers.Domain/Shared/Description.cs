using CSharpFunctionalExtensions;

namespace PetHelpers.Domain.Shared;

public class Description : ComparableValueObject
{
    private Description(string text) => Text = text;

    public string Text { get; }

    public static implicit operator string(Description description) => description.Text;

    public static implicit operator Description(string value) => Create(value).Value;

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