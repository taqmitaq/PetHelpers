using PetHelpers.Application.Dtos;

namespace PetHelpers.Application.Volunteers.UpdateMainInfo;

public record UpdateVolunteerMainInfoRequest(Guid VolunteerId, UpdateVolunteerMainInfoDto Dto);