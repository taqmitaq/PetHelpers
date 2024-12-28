using CSharpFunctionalExtensions;
using PetHelpers.Domain.Shared.Ids;

namespace PetHelpers.Domain.Species;

public sealed class Breed : Entity<BreedId>
{
    public Species Species { get; private set; }
    
    private Breed() { }
    
    public string Title { get; private set; }
}