using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetHelpers.Application.Database;
using PetHelpers.Application.Extensions;
using PetHelpers.Domain.Shared;
using PetHelpers.Domain.Volunteer.ValueObjects;

namespace PetHelpers.Application.Volunteers.UpdateMainInfo;

public class UpdateVolunteerMainInfoHandler
{
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<UpdateVolunteerMainInfoHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UpdateVolunteerMainInfoCommandValidator _validator;

    public UpdateVolunteerMainInfoHandler(
        IVolunteerRepository repository,
        ILogger<UpdateVolunteerMainInfoHandler> logger,
        IUnitOfWork unitOfWork,
        UpdateVolunteerMainInfoCommandValidator validator)
    {
        _repository = repository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        UpdateVolunteerMainInfoCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var volunteerResult = await _repository.GetById(command.VolunteerId, cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var volunteer = volunteerResult.Value;

        var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;

        var email = Email.Create(command.Email).Value;

        var fullName = FullName.Create(command.FullName.FirstName, command.FullName.LastName).Value;

        volunteer.UpdateMainInfo(command.YearsOfExperience, command.Description, phoneNumber, email, fullName);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Updated volunteer with Id: {volunteer.Id}", volunteer.Id.Value);

        return volunteer.Id.Value;
    }
}