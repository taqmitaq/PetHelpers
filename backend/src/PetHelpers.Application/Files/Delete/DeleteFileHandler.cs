using CSharpFunctionalExtensions;
using PetHelpers.Application.Extensions;
using PetHelpers.Application.Providers;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Application.Files.Delete;

public class DeleteFileHandler
{
    private readonly IFileProvider _provider;
    private readonly DeleteFileCommandValidator _validator;

    public DeleteFileHandler(
        IFileProvider provider,
        DeleteFileCommandValidator validator)
    {
        _provider = provider;
        _validator = validator;
    }

    public async Task<UnitResult<ErrorList>> Handle(
        DeleteFileCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (validationResult.IsValid == false)
            return validationResult.ToList();

        var result = await _provider.DeleteFiles([command.FileDto], cancellationToken);

        if (result.IsFailure)
            return result.Error;

        return UnitResult.Success<ErrorList>();
    }
}