using PetHelpers.Application.Abstractions;
using PetHelpers.Application.Dtos;

namespace PetHelpers.Application.Volunteers.Commands.Create;

public record CreateVolunteerCommand(
    int YearsOfExperience,
    string Description,
    string Email,
    string PhoneNumber,
    FullNameDto FullName,
    IEnumerable<SocialMediaDto> SocialMedias,
    IEnumerable<RequisiteDto> Requisites) : ICommand;