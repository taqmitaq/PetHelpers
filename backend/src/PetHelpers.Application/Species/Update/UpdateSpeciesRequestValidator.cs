using FluentValidation;
using PetHelpers.Application.Dtos;
using PetHelpers.Application.Validation;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Application.Species.Update;

public class UpdateSpeciesRequestValidator : AbstractValidator<UpdateSpeciesRequest>
{
    public UpdateSpeciesRequestValidator()
    {
        RuleFor(r => r.SpeciesId).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}

public class UpdateSpeciesDtoValidator : AbstractValidator<UpdateSpeciesDto>
{
    public UpdateSpeciesDtoValidator()
    {
        RuleFor(d => d.Title).MustBeValueObject(Title.Create);
    }
}