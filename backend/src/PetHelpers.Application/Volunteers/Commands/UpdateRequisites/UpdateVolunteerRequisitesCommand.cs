using PetHelpers.Application.Dtos;

namespace PetHelpers.Application.Volunteers.UpdateRequisites;

public record UpdateVolunteerRequisitesCommand(Guid VolunteerId, IEnumerable<RequisiteDto> Requisites);