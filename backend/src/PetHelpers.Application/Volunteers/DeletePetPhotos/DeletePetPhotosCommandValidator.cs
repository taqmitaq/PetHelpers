using FluentValidation;
using PetHelpers.Application.Dtos;
using PetHelpers.Application.Validation;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Application.Volunteers.DeletePetPhotos;

public class DeletePetPhotosCommandValidator : AbstractValidator<DeletePetPhotosCommand>
{
    public DeletePetPhotosCommandValidator()
    {
        RuleFor(c => c.PetId)
            .NotEmpty()
            .WithError(Errors.General.ValueIsInvalid("PetId"));
        RuleForEach(c => c.PhotoDtos).SetValidator(new PhotoDtoValidator());
    }
}

public class PhotoDtoValidator : AbstractValidator<PhotoDto>
{
    public PhotoDtoValidator()
    {
        RuleFor(f => f.PathToStorage)
            .NotEmpty()
            .WithError(Errors.General.ValueIsInvalid("PathToStorage"));
    }
}