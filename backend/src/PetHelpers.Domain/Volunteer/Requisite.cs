using CSharpFunctionalExtensions;

namespace PetHelpers.Domain.Volunteer;

public class Requisite : ValueObject
{
    private Requisite(string title, string description)
    {
        Title = title;
        Description = description;
    }

    public string Title { get; }
    public string Description { get; }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Title;
        yield return Description;
    }
}