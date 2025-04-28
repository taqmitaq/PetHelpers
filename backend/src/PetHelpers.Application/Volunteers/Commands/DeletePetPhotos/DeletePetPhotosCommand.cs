using PetHelpers.Application.Abstractions;
using PetHelpers.Application.Dtos;

namespace PetHelpers.Application.Volunteers.Commands.DeletePetPhotos;

public record DeletePetPhotosCommand(Guid PetId, IEnumerable<PhotoDto> PhotoDtos) : ICommand;