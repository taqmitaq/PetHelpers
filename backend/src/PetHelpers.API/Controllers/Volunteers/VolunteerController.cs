using Microsoft.AspNetCore.Mvc;
using PetHelpers.API.Controllers.Volunteers.Requests;
using PetHelpers.API.Extensions;
using PetHelpers.API.Processors;
using PetHelpers.API.Response;
using PetHelpers.Application.Volunteers.AddPet;
using PetHelpers.Application.Volunteers.AddPetPhotos;
using PetHelpers.Application.Volunteers.ChangePetPosition;
using PetHelpers.Application.Volunteers.Create;
using PetHelpers.Application.Volunteers.Delete;
using PetHelpers.Application.Volunteers.DeletePetPhotos;
using PetHelpers.Application.Volunteers.UpdateMainInfo;
using PetHelpers.Application.Volunteers.UpdateRequisites;
using PetHelpers.Application.Volunteers.UpdateSocialMedias;

namespace PetHelpers.API.Controllers.Volunteers;

public class VolunteerController : ApplicationController
{
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromServices] CreateVolunteerHandler handler,
        [FromBody] CreateVolunteerRequest request,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(Envelope.Success(result.Value));
    }

    [HttpPut("{id:guid}/main-info")]
    public async Task<ActionResult<Guid>> UpdateMainInfo(
        [FromRoute] Guid id,
        [FromBody] UpdateVolunteerMainInfoRequest request,
        [FromServices] UpdateVolunteerMainInfoHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(id), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(Envelope.Success(result.Value));
    }

    [HttpPut("{id:guid}/requisites")]
    public async Task<ActionResult<Guid>> UpdateRequisites(
        [FromRoute] Guid id,
        [FromBody] UpdateVolunteerRequisitesRequest request,
        [FromServices] UpdateVolunteerRequisitesHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(id), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(Envelope.Success(result.Value));
    }

    [HttpPut("{id:guid}/social-medias")]
    public async Task<ActionResult<Guid>> UpdateSocialMedias(
        [FromRoute] Guid id,
        [FromBody] UpdateVolunteerSocialMediasRequest request,
        [FromServices] UpdateVolunteerSocialMediasHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(id), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(Envelope.Success(result.Value));
    }

    [HttpDelete("{id:guid}/hard")]
    public async Task<ActionResult<Guid>> HardDelete(
        [FromRoute] Guid id,
        [FromServices] HardDeleteVolunteerHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new DeleteVolunteerCommand(id);

        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(Envelope.Success(result.Value));
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> SoftDelete(
        [FromRoute] Guid id,
        [FromServices] SoftDeleteVolunteerHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new DeleteVolunteerCommand(id);

        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(Envelope.Success(result.Value));
    }

    [HttpPost("{id:guid}/pet")]
    public async Task<ActionResult<Guid>> AddPet(
        [FromRoute] Guid id,
        [FromBody] AddPetRequest request,
        [FromServices] AddPetHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(id), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(Envelope.Success(result.Value));
    }

    [HttpPut("{id:guid}/pet/positions")]
    public async Task<ActionResult<Guid>> ChangePetPosition(
        [FromRoute] Guid id,
        [FromBody] ChangePetPositionRequest request,
        [FromServices] ChangePetPositionHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(id), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(Envelope.Success());
    }

    [HttpPost("pet/{id:guid}/photo")]
    public async Task<IActionResult> AddPetPhotos(
        [FromRoute] Guid id,
        IFormFileCollection files,
        [FromServices] AddPetPhotosHandler handler,
        CancellationToken cancellationToken)
    {
        await using var fileProcessor = new FormFileProcessor();

        var fileCommands = fileProcessor.Process(files);

        var command = new AddPetPhotosCommand(id, fileCommands);

        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(Envelope.Success(result.Value));
    }

    [HttpDelete("pet/{id:guid}/photo")]
    public async Task<IActionResult> DeletePetPhotos(
        [FromRoute] Guid id,
        [FromBody] DeletePetPhotosRequest request,
        [FromServices] DeletePetPhotosHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(id), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(Envelope.Success(result.Value));
    }
}