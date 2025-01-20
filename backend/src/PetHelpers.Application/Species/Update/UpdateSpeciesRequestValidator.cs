using FluentValidation;
using PetHelpers.Application.Dtos;
using PetHelpers.Application.Validation;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Application.Species.Update;

public class UpdateSpeciesRequestValidator : AbstractValidator<UpdateSpeciesRequest>
{
    public UpdateSpeciesRequestValidator()
    {
        RuleFor(u => u.SpeciesId).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}

public class UpdateSpeciesDtoValidator : AbstractValidator<UpdateSpeciesDto>
{
    public UpdateSpeciesDtoValidator()
    {
        RuleFor(u => u.Title).MustBeValueObject(Title.Create);
    }
}