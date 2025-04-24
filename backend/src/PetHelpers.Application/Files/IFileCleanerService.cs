namespace PetHelpers.Application.Files;

public interface IFileCleanerService
{
    Task Process(CancellationToken cancellationToken);
}