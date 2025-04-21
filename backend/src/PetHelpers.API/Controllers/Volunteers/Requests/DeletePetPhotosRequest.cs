using PetHelpers.Application.Dtos;
using PetHelpers.Application.Volunteers.DeletePetPhotos;

namespace PetHelpers.API.Controllers.Volunteers.Requests;

public record DeletePetPhotosRequest(IEnumerable<PhotoDto> PhotoDtos)
{
    public DeletePetPhotosCommand ToCommand(Guid id) => new(id, PhotoDtos);
}