using CSharpFunctionalExtensions;

namespace PetHelpers.Domain.Volunteer.ValueObjects;

public class Requisite : ValueObject
{
    private Requisite(string title, string description)
    {
        Title = title;
        Description = description;
    }

    public string Title { get; }

    public string Description { get; }

    public static Result<Requisite, string> Create(string title, string description)
    {
        if (string.IsNullOrWhiteSpace(title))
            return "Title cannot be empty.";

        if (string.IsNullOrWhiteSpace(description))
            return "Description cannot be empty.";

        var requisite = new Requisite(title, description);

        return Result.Success<Requisite, string>(requisite);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Title;
        yield return Description;
    }
}