using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetHelpers.Application.Database;
using PetHelpers.Application.Extensions;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Application.Species.Delete;

public class SoftDeleteSpeciesHandler
{
    private readonly ISpeciesRepository _repository;
    private readonly ILogger<SoftDeleteSpeciesHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly DeleteSpeciesCommandValidator _validator;

    public SoftDeleteSpeciesHandler(
        ISpeciesRepository repository,
        ILogger<SoftDeleteSpeciesHandler> logger,
        IUnitOfWork unitOfWork,
        DeleteSpeciesCommandValidator validator)
    {
        _repository = repository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        DeleteSpeciesCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var speciesResult = await _repository.GetById(command.SpeciesId, cancellationToken);

        if (speciesResult.IsFailure)
            return speciesResult.Error.ToErrorList();

        var species = speciesResult.Value;

        species.Delete();

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Deleted species with id: {speciesId}", command.SpeciesId);

        return command.SpeciesId;
    }
}