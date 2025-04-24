using PetHelpers.Domain.Shared;

namespace PetHelpers.Application.Files;

public record FileInfo(FilePath FilePath, string BucketName);