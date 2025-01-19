using FluentValidation;
using PetHelpers.Application.Validation;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Application.Species.Create;

public class CreateSpeciesRequestValidator : AbstractValidator<CreateSpeciesRequest>
{
    public CreateSpeciesRequestValidator()
    {
        RuleFor(c => c.Title).MustBeValueObject(Title.Create);
    }
}