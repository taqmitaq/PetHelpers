using FluentValidation;
using PetHelpers.Application.Validation;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Application.Files.Get;

public class GetFileCommandValidator : AbstractValidator<GetFileCommand>
{
    public GetFileCommandValidator()
    {
        RuleFor(r => r.FileDto.ObjectName)
            .NotEmpty()
            .WithError(Errors.General.ValueIsInvalid("ObjectName"));
        RuleFor(r => r.FileDto.BucketName)
            .NotEmpty()
            .WithError(Errors.General.ValueIsInvalid("BucketName"));
    }
}