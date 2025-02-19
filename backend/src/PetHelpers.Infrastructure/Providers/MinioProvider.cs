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
    private const int EXPIRATION_TIME = 60 * 60 * 24;
    private readonly MinioClient _minioClient;
    private readonly ILogger<MinioProvider> _logger;

    public MinioProvider(MinioClient minioClient, ILogger<MinioProvider> logger)
    {
        _minioClient = minioClient;
        _logger = logger;
    }

    public async Task<Result<string, Error>> Upload(FileData fileData, CancellationToken cancellationToken)
    {
        try
        {
            var bucketExistArgs = new BucketExistsArgs()
                .WithBucket(fileData.BucketName);

            var bucketExist = await _minioClient.BucketExistsAsync(bucketExistArgs, cancellationToken);

            if (bucketExist == false)
            {
                var makeBucketArgs = new MakeBucketArgs()
                    .WithBucket(fileData.BucketName);

                await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
            }

            var putObjectArgs = new PutObjectArgs()
                .WithBucket(fileData.BucketName)
                .WithStreamData(fileData.Stream)
                .WithObjectSize(fileData.Stream.Length)
                .WithObject(fileData.ObjectName);

            var result = await _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);

            _logger.LogInformation("File uploaded in minio");

            return result.ObjectName;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Fail to upload file in minio");

            return Error.Failure("file.upload", "Fail to upload file in minio");
        }
    }

    public async Task<Result<string, Error>> Get(FileData fileData, CancellationToken cancellationToken)
    {
        try
        {
            var presignedGetObjectArgs = new PresignedGetObjectArgs()
                .WithBucket(fileData.BucketName)
                .WithObject(fileData.ObjectName)
                .WithExpiry(EXPIRATION_TIME);

            var result = await _minioClient.PresignedGetObjectAsync(presignedGetObjectArgs);

            _logger.LogInformation("File got from minio");

            return fileData.ObjectName;
        }
        catch(Exception e)
        {
            _logger.LogError(e, "Fail to get file from minio");

            return Error.Failure("file.get", "Fail to get file from minio");
        }
    }

    public async Task<Result<string, Error>> Delete(FileData fileData, CancellationToken cancellationToken)
    {
        try
        {
            var removeObjectArgs = new RemoveObjectArgs()
                .WithBucket(fileData.BucketName)
                .WithObject(fileData.ObjectName);

            await _minioClient.RemoveObjectAsync(removeObjectArgs, cancellationToken);

            _logger.LogInformation("File deleted from minio");

            return fileData.ObjectName;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Fail to delete file in minio");

            return Error.Failure("file.delete", "Fail to delete file in minio");
        }
    }
}