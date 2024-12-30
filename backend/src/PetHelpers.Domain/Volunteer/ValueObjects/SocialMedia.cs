using CSharpFunctionalExtensions;
using PetHelpers.Domain.Shared.ValueObjects;

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

    public static Result<SocialMedia, string> Create(Title title, string link)
    {
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