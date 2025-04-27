using PetHelpers.Application.Dtos;

namespace PetHelpers.Application.Volunteers.AddPet;

public record AddPetCommand(
    Guid VolunteerId,
    double Height,
    double Weight,
    bool IsCastrated,
    bool IsVaccinated,
    DateOnly BirthDate,
    string PetName,
    string Description,
    string Color,
    string HealthInfo,
    LocationDto Location,
    string HelpStatus,
    Guid SpeciesId,
    Guid BreedId,
    string OwnersPhoneNumber);