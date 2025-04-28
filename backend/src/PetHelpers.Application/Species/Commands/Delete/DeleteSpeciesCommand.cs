using PetHelpers.Application.Abstractions;

namespace PetHelpers.Application.Species.Commands.Delete;

public record DeleteSpeciesCommand(Guid SpeciesId) : ICommand;