using PetHelpers.Application.Dtos;

namespace PetHelpers.Application.Volunteers.DeletePetPhotos;

public record DeletePetPhotosCommand(Guid PetId, IEnumerable<PhotoDto> PhotoDtos);