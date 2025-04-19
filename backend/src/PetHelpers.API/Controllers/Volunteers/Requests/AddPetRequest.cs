using PetHelpers.Application.Dtos;
using PetHelpers.Application.Volunteers.AddPet;

namespace PetHelpers.API.Controllers.Volunteers.Requests;

public record AddPetRequest(
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
    string OwnersPhoneNumber)
{
    public AddPetCommand ToCommand(Guid id) => new(
        id,
        Height,
        Weight,
        IsCastrated,
        IsVaccinated,
        BirthDate,
        PetName,
        Description,
        Color,
        HealthInfo,
        Location,
        HelpStatus,
        Species,
        Breed,
        OwnersPhoneNumber);
}