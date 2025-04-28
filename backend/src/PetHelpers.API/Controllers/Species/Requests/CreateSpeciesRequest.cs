using PetHelpers.Application.Species.Commands.Create;

namespace PetHelpers.API.Controllers.Species.Requests;

public record CreateSpeciesRequest(string Title)
{
    public CreateSpeciesCommand ToCommand() => new(Title);
}