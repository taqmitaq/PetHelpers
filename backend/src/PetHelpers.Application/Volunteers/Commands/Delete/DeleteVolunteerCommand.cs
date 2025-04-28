using PetHelpers.Application.Abstractions;

namespace PetHelpers.Application.Volunteers.Commands.Delete;

public record DeleteVolunteerCommand(Guid VolunteerId) : ICommand;