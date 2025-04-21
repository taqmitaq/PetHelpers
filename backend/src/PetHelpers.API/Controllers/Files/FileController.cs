using Microsoft.AspNetCore.Mvc;
using PetHelpers.API.Controllers.Files.Requests;
using PetHelpers.API.Extensions;
using PetHelpers.API.Processors;
using PetHelpers.API.Response;
using PetHelpers.Application.Dtos;
using PetHelpers.Application.Files.Delete;
using PetHelpers.Application.Files.Get;
using PetHelpers.Application.Files.Upload;

namespace PetHelpers.API.Controllers.Files;

public class FileController : ApplicationController
{
    [HttpPost]
    public async Task<IActionResult> Upload(
        IFormFileCollection files,
        [FromServices] UploadFileHandler handler,
        CancellationToken cancellationToken)
    {
        await using var fileProcessor = new FormFileProcessor();
        var fileCommands = fileProcessor.Process(files);

        var command = new UploadFileCommand(fileCommands);

        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(Envelope.Success(result.Value));
    }

    [HttpGet("{bucketName}/{objectName}")]
    public async Task<IActionResult> Get(
        [FromRoute] string bucketName,
        [FromRoute] string objectName,
        [FromServices] GetFileHandler handler,
        CancellationToken cancellationToken)
    {
        var dto = new FileDto(bucketName, objectName);
        var command = new GetFileCommand(dto);

        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(Envelope.Success(result.Value));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(
        [FromServices] DeleteFileHandler handler,
        [FromBody] DeleteFileRequest request,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(Envelope.Success());
    }
}