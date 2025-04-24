using CSharpFunctionalExtensions;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Application.Files;

public interface IFileProvider
{
    Task<Result<IReadOnlyList<FilePath>, Error>> UploadFiles(
        IEnumerable<FileData> filesData,
        CancellationToken cancellationToken = default);

    Task<Result<string, Error>> Get(
        FileInfo info, CancellationToken cancellationToken);

    Task<Result<IReadOnlyList<FilePath>, ErrorList>> DeleteFiles(
        IEnumerable<FileInfo> fileInfos,
        CancellationToken cancellationToken);
}