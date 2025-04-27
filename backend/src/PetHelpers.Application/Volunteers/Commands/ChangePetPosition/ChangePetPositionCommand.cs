namespace PetHelpers.Application.Volunteers.ChangePetPosition;

public record ChangePetPositionCommand(Guid VolunteerId, int CurrentPosition, int TargetPosition);