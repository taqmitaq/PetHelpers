using PetHelpers.Application.Dtos;

namespace PetHelpers.Application.Volunteers.Create;

public record CreateVolunteerCommand(
    int YearsOfExperience,
    string Description,
    string Email,
    string PhoneNumber,
    FullNameDto FullName,
    IEnumerable<SocialMediaDto> SocialMedias,
    IEnumerable<RequisiteDto> Requisites);