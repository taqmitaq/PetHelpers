using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Application.Species.Update;

public class UpdateSpeciesHandler
{
    private readonly ISpeciesRepository _repository;
    private readonly ILogger<UpdateSpeciesHandler> _logger;

    public UpdateSpeciesHandler(
        ISpeciesRepository repository,
        ILogger<UpdateSpeciesHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(
        UpdateSpeciesRequest request,
        CancellationToken cancellationToken)
    {
        var speciesResult = await _repository.GetById(request.SpeciesId, cancellationToken);

        if (speciesResult.IsFailure)
            return speciesResult.Error;

        var species = speciesResult.Value;

        species.UpdateTitle(request.Dto.Title);

        var result = await _repository.Save(species, cancellationToken);

        _logger.LogInformation("Updated species with Id: {species.Id}", species.Id);

        return result;
    }
}