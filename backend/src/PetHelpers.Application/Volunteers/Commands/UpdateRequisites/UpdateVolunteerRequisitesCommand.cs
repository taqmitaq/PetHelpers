using PetHelpers.Application.Abstractions;
using PetHelpers.Application.Dtos;

namespace PetHelpers.Application.Volunteers.Commands.UpdateRequisites;

public record UpdateVolunteerRequisitesCommand(Guid VolunteerId, IEnumerable<RequisiteDto> Requisites) : ICommand;