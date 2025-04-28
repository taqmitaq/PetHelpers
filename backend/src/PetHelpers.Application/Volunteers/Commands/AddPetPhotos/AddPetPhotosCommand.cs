using PetHelpers.Application.Abstractions;
using PetHelpers.Application.Files;

namespace PetHelpers.Application.Volunteers.Commands.AddPetPhotos;

public record AddPetPhotosCommand(Guid PetId, IEnumerable<CreateFileCommand> FileCommands) : ICommand;