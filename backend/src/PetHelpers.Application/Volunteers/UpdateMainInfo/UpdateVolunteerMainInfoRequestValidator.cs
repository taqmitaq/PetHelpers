﻿using FluentValidation;
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
        RuleFor(u => u.YearsOfExperience)
            .GreaterThanOrEqualTo(0)
            .WithError(Errors.General.ValueIsInvalid("YearsOfExperience"));

        RuleFor(u => u.Description).MustBeValueObject(Description.Create);

        RuleFor(u => u.Email).MustBeValueObject(Email.Create);

        RuleFor(u => u.PhoneNumber).MustBeValueObject(PhoneNumber.Create);

        RuleFor(u => u.FullName)
            .MustBeValueObject(dto => FullName.Create(dto.FirstName, dto.LastName));
    }
}