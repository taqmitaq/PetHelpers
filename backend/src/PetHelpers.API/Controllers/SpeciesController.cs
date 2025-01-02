using Microsoft.AspNetCore.Mvc;
using PetHelpers.API.Extensions;
using PetHelpers.Application.Species.CreateSpecies;

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

        return result.ToResponse();
    }
}