using FluentValidation;
using PetHelpers.Application.Validation;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Application.Files.Upload;

public class UploadFileCommandValidator : AbstractValidator<UploadFileCommand>
{
    public UploadFileCommandValidator()
    {
        RuleFor(r => r.FileCommands)
            .ForEach(r =>
            {
                r.Must(c => string.IsNullOrWhiteSpace(c.FileName))
                    .NotEmpty()
                    .WithError(Errors.General.ValueIsInvalid("FileName"));
            });
    }
}