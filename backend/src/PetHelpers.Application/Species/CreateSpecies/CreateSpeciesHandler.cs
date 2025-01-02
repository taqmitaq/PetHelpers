using CSharpFunctionalExtensions;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Application.Species.CreateSpecies;

public class CreateSpeciesHandler
{
    private readonly ISpeciesRepository _speciesRepository;

    public CreateSpeciesHandler(ISpeciesRepository speciesRepository)
    {
        _speciesRepository = speciesRepository;
    }

    public async Task<Result<Guid, Error>> Handle(
        CreateSpeciesRequest request, CancellationToken cancellationToken)
    {
        var titleResult = Title.Create(request.Title);

        if (titleResult.IsFailure)
            return titleResult.Error;

        var species = await _speciesRepository.GetByTitle(titleResult.Value, cancellationToken);

        if (species.IsSuccess)
            return Errors.Species.AlreadyExists();

        var speciesToCreate = new Domain.Species.Entities.Species(request.Title);

        await _speciesRepository.Add(speciesToCreate, cancellationToken);

        return (Guid)speciesToCreate.Id;
    }
}