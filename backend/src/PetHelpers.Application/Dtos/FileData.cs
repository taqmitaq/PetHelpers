using PetHelpers.Domain.Shared;

namespace PetHelpers.Application.Dtos;

public record FileData(Stream Stream, FilePath FilePath, string BucketName);