using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetHelpers.Application.Database;
using PetHelpers.Application.Extensions;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Application.Volunteers.Delete;

public class SoftDeleteVolunteerHandler
{
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<SoftDeleteVolunteerHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly DeleteVolunteerCommandValidator _validator;

    public SoftDeleteVolunteerHandler(
        IVolunteerRepository repository,
        ILogger<SoftDeleteVolunteerHandler> logger,
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

        volunteer.Delete();

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Deleted volunteer with id: {volunteerId}", volunteer.Id.Value);

        return volunteer.Id.Value;
    }
}