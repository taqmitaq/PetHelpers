using CSharpFunctionalExtensions;
using PetHelpers.Application.Providers;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Application.Files.Delete;

public class DeleteFileHandler
{
    private readonly IFileProvider _provider;

    public DeleteFileHandler(IFileProvider provider)
    {
        _provider = provider;
    }

    public async Task<UnitResult<Error>> Handle(
        DeleteFileRequest request,
        CancellationToken cancellationToken)
    {
        return await _provider.Delete(request.FileDto, cancellationToken);
    }
}