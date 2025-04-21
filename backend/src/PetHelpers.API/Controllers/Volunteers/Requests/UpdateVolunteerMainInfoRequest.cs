using PetHelpers.Application.Dtos;
using PetHelpers.Application.Volunteers.UpdateMainInfo;

namespace PetHelpers.API.Controllers.Volunteers.Requests;

public record UpdateVolunteerMainInfoRequest(
    int YearsOfExperience,
    string Description,
    string Email,
    string PhoneNumber,
    FullNameDto FullName)
{
    public UpdateVolunteerMainInfoCommand ToCommand(Guid id) => new(
        id,
        YearsOfExperience,
        Description,
        Email,
        PhoneNumber,
        FullName);
}