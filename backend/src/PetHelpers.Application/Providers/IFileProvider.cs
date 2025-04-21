using CSharpFunctionalExtensions;
using PetHelpers.Application.Dtos;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Application.Providers;

public interface IFileProvider
{
    Task<Result<IReadOnlyList<FilePath>, Error>> UploadFiles(
        IEnumerable<FileData> filesData,
        CancellationToken cancellationToken = default);

    Task<Result<string, Error>> Get(
        FileDto dto, CancellationToken cancellationToken);

    Task<Result<IReadOnlyList<FilePath>, ErrorList>> DeleteFiles(
        IEnumerable<FileDto> fileDtos,
        CancellationToken cancellationToken);
}