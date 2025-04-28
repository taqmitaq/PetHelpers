using PetHelpers.Application.Abstractions;

namespace PetHelpers.Application.Volunteers.Commands.ChangePetPosition;

public record ChangePetPositionCommand(Guid VolunteerId, int CurrentPosition, int TargetPosition) : ICommand;