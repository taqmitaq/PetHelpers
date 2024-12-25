using CSharpFunctionalExtensions;

namespace PetHelpers.Domain.Species;

public class Species : Entity<Guid>
{
    private readonly List<Breed> _breeds = [];
    
    private Species() { }
    
    public string Title { get; private set; }
    public IReadOnlyList<Breed> Breeds => _breeds;
}