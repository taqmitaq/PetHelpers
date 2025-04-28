using PetHelpers.Application.Abstractions;
using PetHelpers.Application.Dtos;

namespace PetHelpers.Application.Volunteers.Commands.UpdateMainInfo;

public record UpdateVolunteerMainInfoCommand(
    Guid VolunteerId,
    int YearsOfExperience,
    string Description,
    string Email,
    string PhoneNumber,
    FullNameDto FullName) : ICommand;