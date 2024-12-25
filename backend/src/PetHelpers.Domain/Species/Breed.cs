using CSharpFunctionalExtensions;

namespace PetHelpers.Domain.Species;

public class Breed : Entity<Guid>
{
    private Breed() { }
    
    public string Title { get; private set; }
}