using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetHelpers.API.Extensions;
using PetHelpers.API.Response;
using PetHelpers.Application.Dtos;
using PetHelpers.Application.Volunteers.Create;
using PetHelpers.Application.Volunteers.UpdateMainInfo;

namespace PetHelpers.API.Controllers;

public class VolunteerController : ApplicationController
{
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromServices] CreateVolunteerHandler handler,
        [FromBody] CreateVolunteerRequest request,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToErrorResponse();

        return Ok(Envelope.Success(result.Value));
    }

    [HttpPut("{id:guid}/main-info")]
    public async Task<ActionResult<Guid>> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateVolunteerMainInfoDto dto,
        [FromServices] UpdateVolunteerMainInfoHandler handler,
        [FromServices] IValidator<UpdateVolunteerMainInfoRequest> validator,
        CancellationToken cancellationToken)
    {
        var request = new UpdateVolunteerMainInfoRequest(id, dto);

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