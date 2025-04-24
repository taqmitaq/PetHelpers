using Microsoft.Extensions.Logging;
using PetHelpers.Application.Files;
using PetHelpers.Application.Messaging;
using FileInfo = PetHelpers.Application.Files.FileInfo;

namespace PetHelpers.Infrastructure.Files;

public class FileCleanerService : IFileCleanerService
{
    private readonly IFileProvider _fileProvider;
    private readonly ILogger<FileCleanerService> _logger;
    private readonly IMessageQueue<IEnumerable<FileInfo>> _messageQueue;

    public FileCleanerService(
        IFileProvider fileProvider,
        ILogger<FileCleanerService> logger,
        IMessageQueue<IEnumerable<FileInfo>> messageQueue)
    {
        _fileProvider = fileProvider;
        _logger = logger;
        _messageQueue = messageQueue;
    }

    public async Task Process(CancellationToken cancellationToken)
    {
        var fileInfos = await _messageQueue.ReadAsync(cancellationToken);

        var result = await _fileProvider.DeleteFiles(fileInfos, cancellationToken);

        if (result.IsFailure)
        {
            _logger.LogError("Failed to delete files: {files}", result.Error);
            return;
        }

        _logger.LogError("Files deleted: {files}", result.Value);
    }
}