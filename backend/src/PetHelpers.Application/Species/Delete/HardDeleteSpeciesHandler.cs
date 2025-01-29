using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Application.Species.Delete;

public class HardDeleteSpeciesHandler
{
    private readonly ISpeciesRepository _repository;
    private readonly ILogger<HardDeleteSpeciesHandler> _logger;

    public HardDeleteSpeciesHandler(
        ISpeciesRepository repository,
        ILogger<HardDeleteSpeciesHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(
        DeleteSpeciesRequest request,
        CancellationToken cancellationToken)
    {
        var speciesResult = await _repository.GetById(request.SpeciesId, cancellationToken);

        if (speciesResult.IsFailure)
            return speciesResult.Error;

        var species = speciesResult.Value;

        var result = await _repository.Delete(species, cancellationToken);

        _logger.LogInformation("Deleted species with id: {speciesId}", result);

        return result;
    }
}