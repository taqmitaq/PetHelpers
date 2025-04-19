using CSharpFunctionalExtensions;
using PetHelpers.Application.Dtos;
using PetHelpers.Application.Extensions;
using PetHelpers.Application.Providers;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Application.Files.Upload;

public class UploadFileHandler
{
    private const string BUCKET_NAME = "files";

    private readonly IFileProvider _provider;
    private readonly UploadFileCommandValidator _validator;

    public UploadFileHandler(
        IFileProvider provider,
        UploadFileCommandValidator validator)
    {
        _provider = provider;
        _validator = validator;
    }

    public async Task<Result<string, ErrorList>> Handle(
        UploadFileCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (validationResult.IsValid == false)
            return validationResult.ToList();

        List<FileData> files = [];

        foreach (var file in command.FileCommands)
        {
            var extension = Path.GetExtension(file.FileName);
            var filePath = FilePath.Create(Guid.NewGuid() + extension);

            if (filePath.IsFailure)
                return filePath.Error.ToErrorList();

            var fileData = new FileData(file.Content, filePath.Value, BUCKET_NAME);

            files.Add(fileData);
        }

        var result = await _provider.UploadFiles(files, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToErrorList();

        return result.Value[0].Path;
    }
}