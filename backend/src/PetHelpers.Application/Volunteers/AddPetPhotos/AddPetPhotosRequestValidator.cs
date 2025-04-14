using FluentValidation;
using PetHelpers.Application.Validation;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Application.Volunteers.AddPetPhotos;

public class AddPetPhotosRequestValidator : AbstractValidator<AddPetPhotosRequest>
{
    public AddPetPhotosRequestValidator()
    {
        RuleFor(r => r.PetId)
            .NotEmpty()
            .WithError(Errors.General.ValueIsInvalid("PetId"));
        RuleFor(r => r.FileData.Stream)
            .NotEmpty()
            .WithError(Errors.General.ValueIsInvalid("Stream"));
        RuleFor(r => r.FileData.ObjectName)
            .NotEmpty()
            .WithError(Errors.General.ValueIsInvalid("ObjectName"));
        RuleFor(r => r.FileData.BucketName)
            .NotEmpty()
            .WithError(Errors.General.ValueIsInvalid("BucketName"));
    }
}