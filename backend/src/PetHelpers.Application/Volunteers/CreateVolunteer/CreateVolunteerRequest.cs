namespace PetHelpers.Application.Volunteers.CreateVolunteer;

public record CreateVolunteerRequest(
    int YearsOfExperience,
    string Description,
    string Email,
    string FirstName,
    string LastName,
    string PhoneNumber,
    IEnumerable<SocialMediaDto> SocialMedias,
    IEnumerable<RequisiteDto> Requisites);