using CSharpFunctionalExtensions;

namespace PetHelpers.Domain.Volunteer;

public class SpeciesAndBreed : ValueObject
{
    private SpeciesAndBreed(Guid speciesId, Guid breedId)
    {
        SpeciesId = speciesId;
        BreedId = breedId;
    }
    
    public Guid SpeciesId { get; }
    public Guid BreedId { get; }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return SpeciesId;
        yield return BreedId;
    }
}