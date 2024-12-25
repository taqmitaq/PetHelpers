using CSharpFunctionalExtensions;

namespace PetHelpers.Domain.Volunteer;

public class SocialMedia : ValueObject
{
    private SocialMedia(string title, string link)
    {
        Title = title;
        Link = link;
    }
    
    public string Title { get; private set; }
    public string Link { get; private set; }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Title;
        yield return Link;
    }
}