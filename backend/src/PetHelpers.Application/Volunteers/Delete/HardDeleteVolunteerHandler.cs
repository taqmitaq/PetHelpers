using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetHelpers.Application.Database;
using PetHelpers.Application.Extensions;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Application.Volunteers.Delete;

public class HardDeleteVolunteerHandler
{
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<HardDeleteVolunteerHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly DeleteVolunteerCommandValidator _validator;

    public HardDeleteVolunteerHandler(
        IVolunteerRepository repository,
        ILogger<HardDeleteVolunteerHandler> logger,
        IUnitOfWork unitOfWork,
        DeleteVolunteerCommandValidator validator)
    {
        _repository = repository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        DeleteVolunteerCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (validationResult.IsValid == false)
            return validationResult.ToList();

        var volunteerResult = await _repository.GetById(command.VolunteerId, cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var volunteer = volunteerResult.Value;

        var result = _repository.Delete(volunteer, cancellationToken);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Deleted volunteer with id: {volunteerId}", result);

        return result;
    }
}