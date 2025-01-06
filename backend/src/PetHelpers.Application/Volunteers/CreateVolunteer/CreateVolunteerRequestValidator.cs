using FluentValidation;
using PetHelpers.Application.Validation;
using PetHelpers.Domain.Shared;
using PetHelpers.Domain.Volunteer.ValueObjects;

namespace PetHelpers.Application.Volunteers.CreateVolunteer;

public class CreateVolunteerRequestValidator : AbstractValidator<CreateVolunteerRequest>
{
    public CreateVolunteerRequestValidator()
    {
        RuleFor(c => c.YearsOfExperience)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Years of experience must be greater or equal to 0");

        RuleFor(c => c.Description).MustBeValueObject(Description.Create);

        RuleFor(c => c.Email).MustBeValueObject(Email.Create);

        RuleFor(c => c.PhoneNumber).MustBeValueObject(PhoneNumber.Create);

        RuleFor(c => new { c.FirstName, c.LastName })
            .MustBeValueObject(f => FullName.Create(f.FirstName, f.LastName));

        RuleForEach(c => c.Requisites)
            .Must(c => Title.Create(c.Title).IsSuccess)
            .Must(c => Description.Create(c.Description).IsSuccess);

        RuleForEach(c => c.SocialMedias)
            .Must(c => Title.Create(c.Title).IsSuccess)
            .Must(c => Link.Create(c.Link).IsSuccess);
    }
}