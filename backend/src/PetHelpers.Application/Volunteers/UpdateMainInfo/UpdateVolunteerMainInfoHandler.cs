using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetHelpers.Domain.Shared;
using PetHelpers.Domain.Volunteer.ValueObjects;

namespace PetHelpers.Application.Volunteers.UpdateMainInfo;

public class UpdateVolunteerMainInfoHandler
{
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<UpdateVolunteerMainInfoHandler> _logger;

    public UpdateVolunteerMainInfoHandler(
        IVolunteerRepository repository,
        ILogger<UpdateVolunteerMainInfoHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(
        UpdateVolunteerMainInfoRequest request,
        CancellationToken cancellationToken)
    {
        var volunteerResult = await _repository.GetById(request.VolunteerId, cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error;

        var volunteer = volunteerResult.Value;

        var phoneNumber = PhoneNumber.Create(request.Dto.PhoneNumber).Value;

        var email = Email.Create(request.Dto.Email).Value;

        var fullName = FullName.Create(request.Dto.FullName.FirstName, request.Dto.FullName.LastName).Value;

        volunteer.UpdateMainInfo(request.Dto.YearsOfExperience, request.Dto.Description, phoneNumber, email, fullName);

        var result = await _repository.Save(volunteer, cancellationToken);

        _logger.LogInformation("Updated volunteer with Id: {volunteer.Id}", volunteer.Id);

        return result;
    }
}