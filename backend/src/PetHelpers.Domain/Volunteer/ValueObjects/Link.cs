using CSharpFunctionalExtensions;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Domain.Volunteer.ValueObjects;

public class Link : ComparableValueObject
{
    public string Text { get; }

    private Link(string text)
    {
        Text = text;
    }

    public static Result<Link, Error> Create(string text)
    {
        if (!Uri.IsWellFormedUriString(text, UriKind.Absolute))
            return Errors.General.ValueIsInvalid(nameof(Link));

        var link = new Link(text);

        return link;
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Text;
    }
}