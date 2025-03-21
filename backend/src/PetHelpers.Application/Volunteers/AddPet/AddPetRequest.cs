using PetHelpers.Application.Dtos;

namespace PetHelpers.Application.Volunteers.AddPet;

public record AddPetRequest(Guid VolunteerId, AddPetDto Dto);