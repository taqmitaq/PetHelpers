using PetHelpers.Application.Dtos;
using PetHelpers.Application.Volunteers.Commands.Create;

namespace PetHelpers.API.Controllers.Volunteers.Requests;

public record CreateVolunteerRequest(
    int YearsOfExperience,
    string Description,
    string Email,
    string PhoneNumber,
    FullNameDto FullName,
    IEnumerable<SocialMediaDto> SocialMedias,
    IEnumerable<RequisiteDto> Requisites)
{
    public CreateVolunteerCommand ToCommand() => new(
        YearsOfExperience,
        Description,
        Email,
        PhoneNumber,
        FullName,
        SocialMedias,
        Requisites);
}