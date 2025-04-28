using PetHelpers.Application.Abstractions;

namespace PetHelpers.Application.Species.Commands.Update;

public record UpdateSpeciesCommand(Guid Id, string Title) : ICommand;