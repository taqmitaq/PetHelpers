using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Application.Species.CreateSpecies;

public class CreateSpeciesHandler
{
    private readonly ISpeciesRepository _speciesRepository;
    private readonly ILogger<CreateSpeciesHandler> _logger;

    public CreateSpeciesHandler(
        ISpeciesRepository speciesRepository,
        ILogger<CreateSpeciesHandler> logger)
    {
        _speciesRepository = speciesRepository;
        _logger = logger;
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

        _logger.LogInformation("Created Species {title}", title);

        return (Guid)speciesToCreate.Id;
    }
}