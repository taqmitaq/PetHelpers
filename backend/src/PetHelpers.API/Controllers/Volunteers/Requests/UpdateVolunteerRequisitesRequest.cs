using PetHelpers.Application.Dtos;
using PetHelpers.Application.Volunteers.UpdateRequisites;

namespace PetHelpers.API.Controllers.Volunteers.Requests;

public record UpdateVolunteerRequisitesRequest(IEnumerable<RequisiteDto> Requisites)
{
    public UpdateVolunteerRequisitesCommand ToCommand(Guid id) => new(id, Requisites);
}