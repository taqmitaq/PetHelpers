using CSharpFunctionalExtensions;

namespace PetHelpers.Domain.Volunteer.ValueObjects;

public class SpeciesAndBreed : ValueObject
{
    private SpeciesAndBreed(Guid speciesId, Guid breedId)
    {
        SpeciesId = speciesId;
        BreedId = breedId;
    }

    public Guid SpeciesId { get; }

    public Guid BreedId { get; }

    public static Result<SpeciesAndBreed, string> Create(Guid speciesId, Guid breedId)
    {
        var speciesAndBreed = new SpeciesAndBreed(speciesId, breedId);

        return Result.Success<SpeciesAndBreed, string>(speciesAndBreed);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return SpeciesId;
        yield return BreedId;
    }
}