using PetHelpers.Application.Species.Update;

namespace PetHelpers.API.Controllers.Species.Requests;

public record UpdateSpeciesRequest(string Title)
{
    public UpdateSpeciesCommand ToCommand(Guid id) => new(id, Title);
}