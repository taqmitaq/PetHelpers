using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using PetHelpers.Application.Dtos;
using PetHelpers.Application.Providers;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Infrastructure.Providers;

public class MinioProvider : IFileProvider
{
    private const int MAX_DEGREE_OF_PARALLELISM = 10;
    private const int EXPIRATION_TIME = 60 * 60 * 24;

    private readonly IMinioClient _minioClient;
    private readonly ILogger<MinioProvider> _logger;

    public MinioProvider(IMinioClient minioClient, ILogger<MinioProvider> logger)
    {
        _minioClient = minioClient;
        _logger = logger;
    }

    public async Task<Result<IReadOnlyList<FilePath>, Error>> UploadFiles(
        IEnumerable<FileData> filesData,
        CancellationToken cancellationToken)
    {
        var semaphoreSlim = new SemaphoreSlim(MAX_DEGREE_OF_PARALLELISM);
        var filesList = filesData.ToList();

        try
        {
            await IfBucketsNotExistCreateBucket(filesList, cancellationToken);

            var tasks = filesList.Select(async file =>
                await PutObject(file, semaphoreSlim, cancellationToken));

            var uploadResult = await Task.WhenAll(tasks);

            if (uploadResult.Any(p => p.IsFailure))
                return uploadResult.First().Error;

            var results = uploadResult.Select(p => p.Value)
                .ToList();

            _logger.LogInformation("Files uploaded to minio");

            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Fail to upload files in minio, files amount: {amount}",
                filesList.Count);

            return Error.Failure("file.upload", "Fail to upload files in minio");
        }
    }

    private async Task<Result<FilePath, Error>> PutObject(
        FileData fileData,
        SemaphoreSlim semaphoreSlim,
        CancellationToken cancellationToken)
    {
        await semaphoreSlim.WaitAsync(cancellationToken);

        var putObjectArgs = new PutObjectArgs()
            .WithBucket(fileData.BucketName)
            .WithStreamData(fileData.Stream)
            .WithObjectSize(fileData.Stream.Length)
            .WithObject(fileData.FilePath.Path);

        try
        {
            await _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);

            return fileData.FilePath;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Fail to upload file in minio with path {path} in bucket {bucket}",
                fileData.FilePath.Path,
                fileData.BucketName);

            return Error.Failure("file.upload", "Fail to upload file in minio");
        }
        finally
        {
            semaphoreSlim.Release();
        }
    }

    private async Task IfBucketsNotExistCreateBucket(
        IEnumerable<FileData> filesData,
        CancellationToken cancellationToken)
    {
        HashSet<string> bucketNames = [..filesData.Select(file => file.BucketName)];

        foreach (var bucketName in bucketNames)
        {
            var bucketExistArgs = new BucketExistsArgs()
                .WithBucket(bucketName);

            var bucketExist = await _minioClient
                .BucketExistsAsync(bucketExistArgs, cancellationToken);

            if (bucketExist is false)
            {
                var makeBucketArgs = new MakeBucketArgs()
                    .WithBucket(bucketName);

                await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
            }
        }
    }

    public async Task<Result<string, Error>> Get(FileDto fileDto, CancellationToken cancellationToken)
    {
        try
        {
            var presignedGetObjectArgs = new PresignedGetObjectArgs()
                .WithBucket(fileDto.BucketName)
                .WithObject(fileDto.ObjectName)
                .WithExpiry(EXPIRATION_TIME);

            var result = await _minioClient.PresignedGetObjectAsync(presignedGetObjectArgs);

            _logger.LogInformation("Link to file got from minio");

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail to get file from minio");

            return Error.Failure("file.get", "Fail to get file from minio");
        }
    }

    public async Task<Result<IReadOnlyList<FilePath>, ErrorList>> DeleteFiles(
        IEnumerable<FileDto> fileDtos,
        CancellationToken cancellationToken)
    {
        var semaphoreSlim = new SemaphoreSlim(MAX_DEGREE_OF_PARALLELISM);
        var filesList = fileDtos.ToList();

        try
        {
            var tasks = filesList.Select(async file =>
                await RemoveObject(file, semaphoreSlim, cancellationToken));

            var removeResult = await Task.WhenAll(tasks);

            if (removeResult.Any(r => r.IsFailure))
                return removeResult.First().Error.ToErrorList();

            var results = removeResult.Select(p => p.Value).ToList();

            _logger.LogInformation("Files removed from minio");

            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Fail to upload files in minio, files amount: {amount}",
                filesList.Count);

            return Error.Failure("file.delete", "Fail to delete file in minio")
                .ToErrorList();
        }
    }

    private async Task<Result<FilePath, Error>> RemoveObject(
        FileDto fileDto,
        SemaphoreSlim semaphoreSlim,
        CancellationToken cancellationToken)
    {
        await semaphoreSlim.WaitAsync(cancellationToken);

        try
        {
            var removeObjectArgs = new RemoveObjectArgs()
                .WithBucket(fileDto.BucketName)
                .WithObject(fileDto.ObjectName);

            await _minioClient.RemoveObjectAsync(removeObjectArgs, cancellationToken);

            var filePath = FilePath.Create(fileDto.ObjectName).Value;

            return filePath;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Fail to delete file in minio with path {path} and bucket {bucket}",
                fileDto.ObjectName,
                fileDto.BucketName);

            return Error.Failure("file.delete", "Fail to delete file in minio");
        }
        finally
        {
            semaphoreSlim.Release();
        }
    }
}