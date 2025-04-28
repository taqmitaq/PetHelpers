using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetHelpers.Application.Abstractions;
using PetHelpers.Application.Database;
using PetHelpers.Application.Extensions;
using PetHelpers.Domain.Shared;
using PetHelpers.Domain.Volunteer.ValueObjects;

namespace PetHelpers.Application.Volunteers.Commands.ChangePetPosition;

public class ChangePetPositionHandler : ICommandHandler<ChangePetPositionCommand>
{
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<ChangePetPositionHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ChangePetPositionCommandValidator _validator;

    public ChangePetPositionHandler(
        IVolunteerRepository repository,
        ILogger<ChangePetPositionHandler> logger,
        IUnitOfWork unitOfWork,
        ChangePetPositionCommandValidator validator)
    {
        _repository = repository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<UnitResult<ErrorList>> Handle(
        ChangePetPositionCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var volunteerResult = await _repository.GetById(command.VolunteerId, cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var volunteer = volunteerResult.Value;
        var currentPosition = Position.Create(command.CurrentPosition).Value;
        var targetPosition = Position.Create(command.TargetPosition).Value;

        var result = volunteer.MovePet(currentPosition, targetPosition);

        if (result.IsFailure)
        {
            return result.Error.ToErrorList();
        }

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation(
            "Pet moved from position {currentPosition} to position {targetPosition}",
            currentPosition.Value,
            targetPosition.Value);

        return UnitResult.Success<ErrorList>();
    }
}