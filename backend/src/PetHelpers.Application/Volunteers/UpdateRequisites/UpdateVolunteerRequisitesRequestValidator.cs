using FluentValidation;
using PetHelpers.Application.Dtos;
using PetHelpers.Application.Validation;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Application.Volunteers.UpdateRequisites;

public class UpdateVolunteerRequisitesRequestValidator : AbstractValidator<UpdateVolunteerRequisitesRequest>
{
    public UpdateVolunteerRequisitesRequestValidator()
    {
        RuleFor(u => u.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}

public class UpdateVolunteerRequisitesDtoValidator : AbstractValidator<UpdateVolunteerRequisitesDto>
{
    public UpdateVolunteerRequisitesDtoValidator()
    {
        RuleFor(u => u.Requisites)
            .ForEach(r =>
            {
                r.Must(dto => Title.Create(dto.Title).IsSuccess)
                    .WithError(Errors.General.ValueIsInvalid("Title"));
                r.Must(dto => Description.Create(dto.Description).IsSuccess)
                    .WithError(Errors.General.ValueIsInvalid("Description"));
            });
    }
}