using CSharpFunctionalExtensions;
using PetHelpers.Application.Providers;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Application.Files.Get;

public class GetFileHandler
{
    private readonly IFileProvider _provider;

    public GetFileHandler(IFileProvider provider)
    {
        _provider = provider;
    }

    public async Task<Result<string, Error>> Handle(
        GetFileRequest request,
        CancellationToken cancellationToken)
    {
        return await _provider.Get(request.FileDto, cancellationToken);
    }
}