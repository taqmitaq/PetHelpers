using CSharpFunctionalExtensions;
using PetHelpers.Domain.Shared.ValueObjects;

namespace PetHelpers.Domain.Volunteer.ValueObjects;

public class SocialMedia : ValueObject
{
    private SocialMedia(string title, string link) //трайнуть конверсию коллекций с типом Title
    {
        Title = title;
        Link = link;
    }

    public string Title { get; }

    public string Link { get; }

    public static Result<SocialMedia, string> Create(string title, string link)
    {
        if (string.IsNullOrWhiteSpace(title))
            return "Title cannot be empty.";

        if (string.IsNullOrWhiteSpace(link))
            return "Link cannot be empty.";

        var socialMedia = new SocialMedia(title, link);

        return Result.Success<SocialMedia, string>(socialMedia);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Title;
        yield return Link;
    }
}