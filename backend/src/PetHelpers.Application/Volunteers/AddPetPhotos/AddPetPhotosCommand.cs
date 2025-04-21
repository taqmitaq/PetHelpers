namespace PetHelpers.Application.Volunteers.AddPetPhotos;

public record AddPetPhotosCommand(Guid PetId, IEnumerable<CreateFileCommand> FileCommands);