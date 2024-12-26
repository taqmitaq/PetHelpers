using CSharpFunctionalExtensions;
using PetHelpers.Domain.Shared.Ids;

namespace PetHelpers.Domain.Species;

public class Breed : Entity<BreedId>
{
    private Breed() { }
    
    public string Title { get; private set; }
}