using Microsoft.AspNetCore.Mvc;

namespace PetHelpers.API.Controllers;

[ApiController]
[Route("[controller]")]
public abstract class ApplicationController : ControllerBase;