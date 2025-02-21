using CSharpFunctionalExtensions;
using PetHelpers.Application.Providers;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Application.Files.Upload;

public class UploadFileHandler
{
    private readonly IFileProvider _provider;

    public UploadFileHandler(IFileProvider provider)
    {
        _provider = provider;
    }

    public async Task<Result<string, Error>> Handle(
        UploadFileRequest request,
        CancellationToken cancellationToken)
    {
        return await _provider.Upload(request.FileData, cancellationToken);
    }
}