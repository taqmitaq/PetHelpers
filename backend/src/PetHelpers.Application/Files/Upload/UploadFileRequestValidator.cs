using FluentValidation;
using PetHelpers.Application.Validation;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Application.Files.Upload;

public class UploadFileRequestValidator : AbstractValidator<UploadFileRequest>
{
    public UploadFileRequestValidator()
    {
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