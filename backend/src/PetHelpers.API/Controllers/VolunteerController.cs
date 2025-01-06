using Microsoft.AspNetCore.Mvc;
using PetHelpers.API.Extensions;
using PetHelpers.API.Response;
using PetHelpers.Application.Volunteers.CreateVolunteer;

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
}