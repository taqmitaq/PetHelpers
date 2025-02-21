using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Application.Species.Create;

public class CreateSpeciesHandler
{
    private readonly ISpeciesRepository _repository;
    private readonly ILogger<CreateSpeciesHandler> _logger;

    public CreateSpeciesHandler(
        ISpeciesRepository repository,
        ILogger<CreateSpeciesHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(
        CreateSpeciesRequest request,
        CancellationToken cancellationToken)
    {
        var title = Title.Create(request.Title).Value;

        var species = await _repository.GetByTitle(title, cancellationToken);

        if (species.IsSuccess)
            return Errors.Species.AlreadyExists();

        var speciesToCreate = new Domain.Species.Species(request.Title);

        var result = await _repository.Add(speciesToCreate, cancellationToken);

        _logger.LogInformation("Created Species {title} with id {speciesId}", title.Text, result);

        return result;
    }
}