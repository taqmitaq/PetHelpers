﻿namespace PetHelpers.Infrastructure.Options;

public class MinioOptions
{
    public const string MINIO = "MINIO";

    public string Endpoint { get; init; } = string.Empty;

    public string AccessKey { get; init; } = string.Empty;

    public string SecretKey { get; init; } = string.Empty;

    public bool WithSSL { get; init; } = false;
}