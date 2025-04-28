using PetHelpers.Application.Abstractions;

namespace PetHelpers.Application.Species.Commands.Create;

public record CreateSpeciesCommand(string Title) : ICommand;