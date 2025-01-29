using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Application.Species.Delete;

public class SoftDeleteSpeciesHandler
{
    private readonly ISpeciesRepository _repository;
    private readonly ILogger<SoftDeleteSpeciesHandler> _logger;

    public SoftDeleteSpeciesHandler(
        ISpeciesRepository repository,
        ILogger<SoftDeleteSpeciesHandler> logger)
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

        species.Delete();

        var result = await _repository.Save(species, cancellationToken);

        _logger.LogInformation("Deleted species with id: {speciesId}", result);

        return result;
    }
}