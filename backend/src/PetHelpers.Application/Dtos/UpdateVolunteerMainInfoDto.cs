namespace PetHelpers.Application.Dtos;

public record UpdateVolunteerMainInfoDto(
    int YearsOfExperience,
    string Description,
    string Email,
    string PhoneNumber,
    FullNameDto FullName);