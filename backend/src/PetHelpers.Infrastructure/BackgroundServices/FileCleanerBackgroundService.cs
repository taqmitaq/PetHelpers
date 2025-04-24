using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PetHelpers.Application.Files;

namespace PetHelpers.Infrastructure.BackgroundServices;

public class FileCleanerBackgroundService : BackgroundService
{
    private readonly ILogger<FileCleanerBackgroundService> _logger;
    private readonly IServiceScopeFactory _scopeFactory;

    public FileCleanerBackgroundService(
        ILogger<FileCleanerBackgroundService> logger,
        IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("FileCleanerBackgroundService is starting.");

        await using var scope = _scopeFactory.CreateAsyncScope();

        var fileCleanerService = scope.ServiceProvider.GetRequiredService<IFileCleanerService>();

        while (!stoppingToken.IsCancellationRequested)
        {
            await fileCleanerService.Process(stoppingToken);
        }

        await Task.CompletedTask;
    }
}