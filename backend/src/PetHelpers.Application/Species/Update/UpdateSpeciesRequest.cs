using PetHelpers.Application.Dtos;

namespace PetHelpers.Application.Species.Update;

public record UpdateSpeciesRequest(Guid SpeciesId, UpdateSpeciesDto Dto);