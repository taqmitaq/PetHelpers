using CSharpFunctionalExtensions;
using PetHelpers.Application.Extensions;
using PetHelpers.Application.Providers;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Application.Files.Get;

public class GetFileHandler
{
    private readonly IFileProvider _provider;
    private readonly GetFileCommandValidator _validator;

    public GetFileHandler(
        IFileProvider provider,
        GetFileCommandValidator validator)
    {
        _provider = provider;
        _validator = validator;
    }

    public async Task<Result<string, ErrorList>> Handle(
        GetFileCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (validationResult.IsValid == false)
            return validationResult.ToList();

        var result = await _provider.Get(command.FileDto, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToErrorList();

        return result.Value;
    }
}