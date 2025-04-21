using FluentValidation;
using PetHelpers.Application.Validation;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Application.Species.Update;

public class UpdateSpeciesCommandValidator : AbstractValidator<UpdateSpeciesCommand>
{
    public UpdateSpeciesCommandValidator()
    {
        RuleFor(u => u.Id)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired());
        RuleFor(u => u.Title).MustBeValueObject(Title.Create);
    }
}