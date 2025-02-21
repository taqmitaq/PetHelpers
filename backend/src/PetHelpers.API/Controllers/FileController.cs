using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetHelpers.API.Extensions;
using PetHelpers.API.Response;
using PetHelpers.Application.Dtos;
using PetHelpers.Application.Files.Delete;
using PetHelpers.Application.Files.Get;
using PetHelpers.Application.Files.Upload;

namespace PetHelpers.API.Controllers;

public class FileController : ApplicationController
{
    [HttpPost]
    public async Task<IActionResult> Upload(
        IFormFile file,
        [FromForm] FileDto fileDto,
        [FromServices] UploadFileHandler handler,
        [FromServices] IValidator<UploadFileRequest> validator,
        CancellationToken cancellationToken)
    {
        await using var stream = file.OpenReadStream();

        var fileData = new FileData(stream, fileDto.BucketName, fileDto.ObjectName);
        var request = new UploadFileRequest(fileData);

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid == false)
        {
            return validationResult.ToValidationErrorResponse();
        }

        var result = await handler.Handle(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToErrorResponse();

        return Ok(Envelope.Success(result.Value));
    }

    [HttpGet("{bucketName}/{objectName}")]
    public async Task<IActionResult> Get(
        [FromRoute] string bucketName,
        [FromRoute] string objectName,
        [FromServices] GetFileHandler handler,
        [FromServices] IValidator<GetFileRequest> validator,
        CancellationToken cancellationToken)
    {
        var dto = new FileDto(bucketName, objectName);
        var request = new GetFileRequest(dto);

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid == false)
        {
            return validationResult.ToValidationErrorResponse();
        }

        var result = await handler.Handle(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToErrorResponse();

        return Ok(Envelope.Success(result.Value));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(
        [FromServices] DeleteFileHandler handler,
        [FromBody] DeleteFileRequest request,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToErrorResponse();

        return Ok(Envelope.Success());
    }
}