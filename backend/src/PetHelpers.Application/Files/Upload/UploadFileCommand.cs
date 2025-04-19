using PetHelpers.Application.Volunteers.AddPetPhotos;

namespace PetHelpers.Application.Files.Upload;

public record UploadFileCommand(IEnumerable<CreateFileCommand> FileCommands);