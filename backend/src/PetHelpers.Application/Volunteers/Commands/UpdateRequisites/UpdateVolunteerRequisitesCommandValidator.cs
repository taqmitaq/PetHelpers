using FluentValidation;
using PetHelpers.Application.Validation;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Application.Volunteers.Commands.UpdateRequisites;

public class UpdateVolunteerRequisitesCommandValidator : AbstractValidator<UpdateVolunteerRequisitesCommand>
{
    public UpdateVolunteerRequisitesCommandValidator()
    {
        RuleFor(u => u.VolunteerId)
            .NotEmpty().WithError(Errors.General.ValueIsRequired());

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