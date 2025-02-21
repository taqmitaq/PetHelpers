using CSharpFunctionalExtensions;
using PetHelpers.Application.Dtos;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Application.Providers;

public interface IFileProvider
{
    public Task<Result<string, Error>> Upload(
        FileData fileData, CancellationToken cancellationToken);

    public Task<Result<string, Error>> Get(
        FileDto dto, CancellationToken cancellationToken);

    public Task<UnitResult<Error>> Delete(
        FileDto dto, CancellationToken cancellationToken);
}