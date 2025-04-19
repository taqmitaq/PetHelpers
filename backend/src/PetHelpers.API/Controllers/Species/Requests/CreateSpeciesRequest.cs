using PetHelpers.Application.Species.Create;

namespace PetHelpers.API.Controllers.Species.Requests;

public record CreateSpeciesRequest(string Title)
{
    public CreateSpeciesCommand ToCommand() => new(Title);
}