using CSharpFunctionalExtensions;

namespace PetHelpers.Domain.Volunteer;

public class SocialMedia : ValueObject
{
    private SocialMedia(string title, string link)
    {
        Title = title;
        Link = link;
    }

    public string Title { get; }
    public string Link { get; }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Title;
        yield return Link;
    }
}