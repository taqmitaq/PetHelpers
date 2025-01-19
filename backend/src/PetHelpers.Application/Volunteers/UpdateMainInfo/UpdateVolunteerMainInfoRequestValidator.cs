using FluentValidation;
using PetHelpers.Application.Dtos;
using PetHelpers.Application.Validation;
using PetHelpers.Domain.Shared;
using PetHelpers.Domain.Volunteer.ValueObjects;

namespace PetHelpers.Application.Volunteers.UpdateMainInfo;

public class UpdateVolunteerMainInfoRequestValidator : AbstractValidator<UpdateVolunteerMainInfoRequest>
{
    public UpdateVolunteerMainInfoRequestValidator()
    {
        RuleFor(u => u.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}

public class UpdateVolunteerMainInfoDtoValidator : AbstractValidator<UpdateVolunteerMainInfoDto>
{
    public UpdateVolunteerMainInfoDtoValidator()
    {
        RuleFor(d => d.YearsOfExperience)
            .GreaterThanOrEqualTo(0)
            .WithError(Errors.General.ValueIsInvalid("YearsOfExperience"));

        RuleFor(d => d.Description).MustBeValueObject(Description.Create);

        RuleFor(d => d.Email).MustBeValueObject(Email.Create);

        RuleFor(d => d.PhoneNumber).MustBeValueObject(PhoneNumber.Create);

        RuleFor(d => d.FullName)
            .MustBeValueObject(dto => FullName.Create(dto.FirstName, dto.LastName));
    }
}