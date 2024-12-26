using CSharpFunctionalExtensions;
using PetHelpers.Domain.Shared.Ids;

namespace PetHelpers.Domain.Species;

public class Species : Entity<SpeciesId>
{
    private readonly List<Breed> _breeds = [];
    
    private Species() { }
    
    public string Title { get; private set; }
    public IReadOnlyList<Breed> Breeds => _breeds;
}