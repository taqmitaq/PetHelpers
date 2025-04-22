using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetHelpers.Application.Database;
using PetHelpers.Application.Extensions;
using PetHelpers.Domain.Shared;
using PetHelpers.Domain.Volunteer;
using PetHelpers.Domain.Volunteer.ValueObjects;

namespace PetHelpers.Application.Volunteers.Create;

public class CreateVolunteerHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<CreateVolunteerHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly CreateVolunteerCommandValidator _validator;

    public CreateVolunteerHandler(
        IVolunteerRepository volunteerRepository,
        ILogger<CreateVolunteerHandler> logger,
        IUnitOfWork unitOfWork,
        CreateVolunteerCommandValidator validator)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        CreateVolunteerCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var description = Description.Create(command.Description).Value;

        var email = Email.Create(command.Email).Value;

        var fullName = FullName.Create(command.FullName.FirstName, command.FullName.LastName).Value;

        var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;

        var volunteerResult = Volunteer.Create(
            command.YearsOfExperience,
            description,
            email,
            fullName,
            phoneNumber);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var volunteer = volunteerResult.Value;

        foreach (var sm in command.SocialMedias)
        {
            var title = Title.Create(sm.Title).Value;

            var link = Link.Create(sm.Link).Value;

            volunteer.AddSocialMedia(new SocialMedia(title, link));
        }

        foreach (var r in command.Requisites)
        {
            var title = Title.Create(r.Title).Value;

            var requisiteDescription = Description.Create(r.Description).Value;

            volunteer.AddRequisite(new Requisite(title, requisiteDescription));
        }

        var result = await _volunteerRepository.Add(volunteer, cancellationToken);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Created volunteer with Id: {volunteerId}", result);

        return result;
    }
}