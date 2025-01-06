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
        var description = Description.Create(request.Description).Value;

        var email = Email.Create(request.Email).Value;

        var fullName = FullName.Create(request.FirstName, request.LastName).Value;

        var phoneNumber = PhoneNumber.Create(request.PhoneNumber).Value;

        var volunteerResult = Volunteer.Create(
            request.YearsOfExperience,
            description,
            email,
            fullName,
            phoneNumber);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error;

        var volunteer = volunteerResult.Value;

        foreach (var sm in request.SocialMedias)
        {
            var title = Title.Create(sm.Title).Value;

            var link = Link.Create(sm.Link).Value;

            volunteer.AddSocialMedia(new SocialMedia(title, link));
        }

        foreach (var r in request.Requisites)
        {
            var title = Title.Create(r.Title).Value;

            var requisiteDescription = Description.Create(r.Description).Value;

            volunteer.AddRequisite(new Requisite(title, requisiteDescription));
        }

        await _volunteerRepository.Add(volunteer, cancellationToken);

        return (Guid)volunteer.Id;
    }
}