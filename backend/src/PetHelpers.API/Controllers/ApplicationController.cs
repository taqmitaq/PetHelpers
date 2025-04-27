using Microsoft.AspNetCore.Mvc;
using PetHelpers.API.Response;

namespace PetHelpers.API.Controllers;

[ApiController]
[Route("[controller]")]
public abstract class ApplicationController : ControllerBase
{
    public override OkObjectResult Ok(object? value)
    {
        var envelope = Envelope.Success(value);

        return base.Ok(envelope);
    }
}