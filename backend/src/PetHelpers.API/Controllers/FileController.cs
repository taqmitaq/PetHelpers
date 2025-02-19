using Microsoft.AspNetCore.Mvc;
using Minio;
using PetHelpers.Application.Dtos;

namespace PetHelpers.API.Controllers;

public class FileController : ApplicationController
{
    private readonly IMinioClient _minioClient;


    public FileController(IMinioClient minioClient)
    {
        _minioClient = minioClient;
    }

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file, CancellationToken cancellationToken)
    {
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Get(FileData fileData, CancellationToken cancellationToken)
    {
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Delete(FileData fileData, CancellationToken cancellationToken)
    {
        return Ok();
    }
}