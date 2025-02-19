using CSharpFunctionalExtensions;
using PetHelpers.Application.Dtos;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Application.Providers;

public interface IFileProvider
{
    public Task<Result<string, Error>> Upload(
        FileData fileData, CancellationToken cancellationToken);

    public Task<Result<string, Error>> Get(
        FileData fileData, CancellationToken cancellationToken);

    public Task<Result<string, Error>> Delete(
        FileData fileData, CancellationToken cancellationToken);
}