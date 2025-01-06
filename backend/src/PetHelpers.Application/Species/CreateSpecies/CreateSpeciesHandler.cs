using CSharpFunctionalExtensions;
using FluentValidation;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Application.Species.CreateSpecies;

public class CreateSpeciesHandler
{
    private readonly ISpeciesRepository _speciesRepository;

    public CreateSpeciesHandler(
        ISpeciesRepository speciesRepository)
    {
        _speciesRepository = speciesRepository;
    }

    public async Task<Result<Guid, Error>> Handle(
        CreateSpeciesRequest request, CancellationToken cancellationToken)
    {
        var title = Title.Create(request.Title).Value;

        var species = await _speciesRepository.GetByTitle(title, cancellationToken);

        if (species.IsSuccess)
            return Errors.Species.AlreadyExists();

        var speciesToCreate = new Domain.Species.Entities.Species(request.Title);

        await _speciesRepository.Add(speciesToCreate, cancellationToken);

        return (Guid)speciesToCreate.Id;
    }
}