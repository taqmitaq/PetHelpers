using PetHelpers.Application.Dtos;

namespace PetHelpers.Application.Volunteers.AddPetPhotos;

public record AddPetPhotosRequest(Guid PetId, FileData FileData);