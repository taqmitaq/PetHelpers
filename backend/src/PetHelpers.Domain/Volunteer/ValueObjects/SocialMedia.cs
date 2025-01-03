using CSharpFunctionalExtensions;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Domain.Volunteer.ValueObjects;

public class SocialMedia : ValueObject
{
    public SocialMedia(Title title, Link link)
    {
        Title = title;
        Link = link;
    }

    public Title Title { get; }

    public Link Link { get; }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Title;
        yield return Link;
    }
}