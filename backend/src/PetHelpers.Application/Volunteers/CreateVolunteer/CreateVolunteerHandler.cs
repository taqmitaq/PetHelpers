using CSharpFunctionalExtensions;
using PetHelpers.Domain.Shared;
using PetHelpers.Domain.Volunteer.Entities;
using PetHelpers.Domain.Volunteer.ValueObjects;

namespace PetHelpers.Application.Volunteers.CreateVolunteer;

public class CreateVolunteerHandler
{
    private readonly IVolunteerRepository _volunteerRepository;

    public CreateVolunteerHandler(IVolunteerRepository volunteerRepository)
    {
        _volunteerRepository = volunteerRepository;
    }

    public async Task<Result<Guid, Error>> Handle(
        CreateVolunteerRequest request, CancellationToken cancellationToken)
    {
        var descriptionResult = Description.Create(request.Description);

        if (descriptionResult.IsFailure)
            return descriptionResult.Error;

        var emailResult = Email.Create(request.Email);

        if (emailResult.IsFailure)
            return emailResult.Error;

        var fullNameResult = FullName.Create(request.FirstName, request.LastName);

        if (fullNameResult.IsFailure)
            return fullNameResult.Error;

        var phoneNumberResult = PhoneNumber.Create(request.PhoneNumber);

        if (phoneNumberResult.IsFailure)
            return phoneNumberResult.Error;

        var volunteerResult = Volunteer.Create(
            request.YearsOfExperience,
            descriptionResult.Value,
            emailResult.Value,
            fullNameResult.Value,
            phoneNumberResult.Value);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error;

        var volunteer = volunteerResult.Value;

        foreach (var sm in request.SocialMedias)
        {
            var titleResult = Title.Create(sm.Title);

            if (titleResult.IsFailure)
                return titleResult.Error;

            var linkResult = Link.Create(sm.Link);

            if (linkResult.IsFailure)
                return linkResult.Error;

            volunteer.AddSocialMedia(new SocialMedia(titleResult.Value, linkResult.Value));
        }

        foreach (var r in request.Requisites)
        {
            var titleResult = Title.Create(r.Title);

            if (titleResult.IsFailure)
                return titleResult.Error;

            var requisiteDescriptionResult = Description.Create(r.Description);

            if (requisiteDescriptionResult.IsFailure)
                return requisiteDescriptionResult.Error;

            var requisite = new Requisite(titleResult.Value, requisiteDescriptionResult.Value);

            volunteer.AddRequisite(requisite);
        }

        await _volunteerRepository.Add(volunteer, cancellationToken);

        return (Guid)volunteer.Id;
    }
}