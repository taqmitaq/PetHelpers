namespace PetHelpers.Application.Volunteers.AddPetPhotos;

public record CreateFileCommand(Stream Content, string FileName);