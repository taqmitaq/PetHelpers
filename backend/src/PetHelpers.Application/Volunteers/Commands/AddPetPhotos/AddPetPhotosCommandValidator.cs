using FluentValidation;
using PetHelpers.Application.Files;
using PetHelpers.Application.Validation;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Application.Volunteers.Commands.AddPetPhotos;

public class AddPetPhotosCommandValidator : AbstractValidator<AddPetPhotosCommand>
{
    public AddPetPhotosCommandValidator()
    {
        RuleFor(r => r.PetId)
            .NotEmpty()
            .WithError(Errors.General.ValueIsInvalid("PetId"));
        RuleForEach(r => r.FileCommands).SetValidator(new CreateFileCommandValidator());
    }
}

public class CreateFileCommandValidator : AbstractValidator<CreateFileCommand>
{
    public CreateFileCommandValidator()
    {
        RuleFor(c => c.FileName).MustBeValueObject(FilePath.Create);
    }
}