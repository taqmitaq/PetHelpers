using FluentValidation;
using PetHelpers.Application.Validation;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Application.Species.Delete;

public class DeleteSpeciesRequestValidator : AbstractValidator<DeleteSpeciesRequest>
{
    public DeleteSpeciesRequestValidator()
    {
        RuleFor(x => x.SpeciesId).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}