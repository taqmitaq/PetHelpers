using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetHelpers.Application.Database;
using PetHelpers.Application.Extensions;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Application.Species.Create;

public class CreateSpeciesHandler
{
    private readonly ISpeciesRepository _repository;
    private readonly ILogger<CreateSpeciesHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly CreateSpeciesCommandValidator _validator;

    public CreateSpeciesHandler(
        ISpeciesRepository repository,
        ILogger<CreateSpeciesHandler> logger,
        IUnitOfWork unitOfWork,
        CreateSpeciesCommandValidator validator)
    {
        _repository = repository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        CreateSpeciesCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (validationResult.IsValid == false)
            return validationResult.ToList();

        var title = Title.Create(command.Title).Value;

        var speciesResult = await _repository.GetByTitle(title, cancellationToken);

        if (speciesResult.IsSuccess)
            return Errors.Species.AlreadyExists().ToErrorList();

        var speciesToCreate = new Domain.Species.Species(command.Title);

        var result = await _repository.Add(speciesToCreate, cancellationToken);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Created Species {title} with id {speciesId}", title.Text, result);

        return result;
    }
}