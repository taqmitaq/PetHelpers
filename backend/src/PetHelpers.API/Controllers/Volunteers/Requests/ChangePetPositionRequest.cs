using PetHelpers.Application.Volunteers.Commands.ChangePetPosition;

namespace PetHelpers.API.Controllers.Volunteers.Requests;

public record ChangePetPositionRequest(int CurrentPosition, int TargetPosition)
{
    public ChangePetPositionCommand ToCommand(Guid id) => new(id, CurrentPosition, TargetPosition);
}