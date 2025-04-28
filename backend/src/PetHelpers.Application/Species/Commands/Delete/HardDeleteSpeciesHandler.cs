using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetHelpers.Application.Abstractions;
using PetHelpers.Application.Database;
using PetHelpers.Application.Extensions;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Application.Species.Commands.Delete;

public class HardDeleteSpeciesHandler : ICommandHandler<Guid, DeleteSpeciesCommand>
{
    private readonly ISpeciesRepository _repository;
    private readonly ILogger<HardDeleteSpeciesHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly DeleteSpeciesCommandValidator _validator;

    public HardDeleteSpeciesHandler(
        ISpeciesRepository repository,
        ILogger<HardDeleteSpeciesHandler> logger,
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

        var result = _repository.Delete(species, cancellationToken);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Deleted species with id: {speciesId}", result);

        return result;
    }
}