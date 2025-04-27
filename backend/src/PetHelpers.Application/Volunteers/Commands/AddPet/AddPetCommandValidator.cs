using FluentValidation;
using PetHelpers.Application.Validation;
using PetHelpers.Domain.Shared;
using PetHelpers.Domain.Volunteer.ValueObjects;

namespace PetHelpers.Application.Volunteers.AddPet;

public class AddPetCommandValidator : AbstractValidator<AddPetCommand>
{
    public AddPetCommandValidator()
    {
        RuleFor(a => a.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());

        RuleFor(a => a.Height)
            .GreaterThan(0)
            .WithError(Errors.General.ValueIsInvalid("Height"));

        RuleFor(a => a.Weight)
            .GreaterThan(0)
            .WithError(Errors.General.ValueIsInvalid("Height"));

        RuleFor(a => a.PetName).MustBeValueObject(PetName.Create);

        RuleFor(a => a.Description).MustBeValueObject(Description.Create);

        RuleFor(a => a.Color).MustBeValueObject(Color.Create);

        RuleFor(a => a.HealthInfo).MustBeValueObject(HealthInfo.Create);

        RuleFor(a => a.Location)
            .MustBeValueObject(dto => Location.Create(dto.City, dto.Region, dto.PostalCode));

        RuleFor(a => a.OwnersPhoneNumber).MustBeValueObject(PhoneNumber.Create);
    }
}