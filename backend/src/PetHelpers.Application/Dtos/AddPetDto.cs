namespace PetHelpers.Application.Dtos;

public record AddPetDto(
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
    string Species,
    string Breed,
    string OwnersPhoneNumber);