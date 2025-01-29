using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetHelpers.API.Extensions;
using PetHelpers.API.Response;
using PetHelpers.Application.Dtos;
using PetHelpers.Application.Species.Create;
using PetHelpers.Application.Species.Delete;
using PetHelpers.Application.Species.Update;

namespace PetHelpers.API.Controllers;

public class SpeciesController : ApplicationController
{
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromServices] CreateSpeciesHandler handler,
        [FromBody] CreateSpeciesRequest request,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToErrorResponse();

        return Ok(Envelope.Success(result.Value));
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Guid>> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateSpeciesDto dto,
        [FromServices] UpdateSpeciesHandler handler,
        [FromServices] IValidator<UpdateSpeciesRequest> validator,
        CancellationToken cancellationToken)
    {
        var request = new UpdateSpeciesRequest(id, dto);

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

    [HttpDelete("{id:guid}/hard")]
    public async Task<ActionResult<Guid>> HardDelete(
        [FromRoute] Guid id,
        [FromServices] HardDeleteSpeciesHandler handler,
        [FromServices] IValidator<DeleteSpeciesRequest> validator,
        CancellationToken cancellationToken)
    {
        var request = new DeleteSpeciesRequest(id);

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

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> SoftDelete(
        [FromRoute] Guid id,
        [FromServices] SoftDeleteSpeciesHandler handler,
        [FromServices] IValidator<DeleteSpeciesRequest> validator,
        CancellationToken cancellationToken)
    {
        var request = new DeleteSpeciesRequest(id);

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
}