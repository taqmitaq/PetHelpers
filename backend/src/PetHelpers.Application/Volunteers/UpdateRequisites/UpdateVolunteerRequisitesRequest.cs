using PetHelpers.Application.Dtos;

namespace PetHelpers.Application.Volunteers.UpdateRequisites;

public record UpdateVolunteerRequisitesRequest(Guid VolunteerId, UpdateVolunteerRequisitesDto Dto);