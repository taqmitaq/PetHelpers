using CSharpFunctionalExtensions;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Domain.Volunteer.ValueObjects;

public class Requisite : ValueObject
{
    public Requisite(Title title, Description description)
    {
        Title = title;
        Description = description;
    }

    public Title Title { get; }

    public Description Description { get; }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Title;
        yield return Description;
    }
}