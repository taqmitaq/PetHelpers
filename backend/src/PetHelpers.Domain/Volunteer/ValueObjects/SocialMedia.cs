using CSharpFunctionalExtensions;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Domain.Volunteer.ValueObjects;

public class SocialMedia : ValueObject
{
    private SocialMedia(Title title, string link)
    {
        Title = title;
        Link = link;
    }

    public Title Title { get; }

    public string Link { get; }

    public static Result<SocialMedia, Error> Create(Title title, string link)
    {
        if (string.IsNullOrWhiteSpace(link))
            return Errors.General.ValueIsRequired(nameof(Link));

        var socialMedia = new SocialMedia(title, link);

        return socialMedia;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Title;
        yield return Link;
    }
}