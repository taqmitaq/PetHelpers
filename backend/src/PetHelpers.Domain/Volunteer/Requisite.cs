using CSharpFunctionalExtensions;

namespace PetHelpers.Domain.Volunteer;

public class Requisite : ValueObject
{
    private Requisite(string title, string description)
    {
        Title = title;
        Description = description;
    }
    
    public string Title { get; private set; }
    public string Description { get; private set; }
    
    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Title;
        yield return Description;
    }
}