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
            .WithError(Errors.General.ValueIsInvalid("YearsOfExperience"));

        RuleFor(c => c.Description).MustBeValueObject(Description.Create);

        RuleFor(c => c.Email).MustBeValueObject(Email.Create);

        RuleFor(c => c.PhoneNumber).MustBeValueObject(PhoneNumber.Create);

        RuleFor(c => new { c.FirstName, c.LastName })
            .MustBeValueObject(f => FullName.Create(f.FirstName, f.LastName));

        RuleFor(c => c.Requisites)
            .ForEach(r =>
            {
                r.Must(dto => Title.Create(dto.Title).IsSuccess)
                    .WithError(Errors.General.ValueIsInvalid("Title"));
                r.Must(dto => Description.Create(dto.Description).IsSuccess)
                    .WithError(Errors.General.ValueIsInvalid("Description"));
            });

        RuleFor(c => c.SocialMedias)
            .ForEach(r =>
            {
                r.Must(dto => Title.Create(dto.Title).IsSuccess)
                    .WithError(Errors.General.ValueIsInvalid("Title"));
                r.Must(dto => Link.Create(dto.Link).IsSuccess)
                    .WithError(Errors.General.ValueIsInvalid("Link"));
            });
    }
}