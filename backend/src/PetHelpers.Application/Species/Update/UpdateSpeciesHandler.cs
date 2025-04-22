using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetHelpers.Application.Database;
using PetHelpers.Application.Extensions;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Application.Species.Update;

public class UpdateSpeciesHandler
{
    private readonly ISpeciesRepository _repository;
    private readonly ILogger<UpdateSpeciesHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UpdateSpeciesCommandValidator _validator;

    public UpdateSpeciesHandler(
        ISpeciesRepository repository,
        ILogger<UpdateSpeciesHandler> logger,
        IUnitOfWork unitOfWork,
        UpdateSpeciesCommandValidator validator)
    {
        _repository = repository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        UpdateSpeciesCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var speciesResult = await _repository.GetById(command.Id, cancellationToken);

        if (speciesResult.IsFailure)
            return speciesResult.Error.ToErrorList();

        var species = speciesResult.Value;

        species.UpdateTitle(command.Title);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Updated species with Id: {speciesId}", species.Id.Value);

        return species.Id.Value;
    }
}