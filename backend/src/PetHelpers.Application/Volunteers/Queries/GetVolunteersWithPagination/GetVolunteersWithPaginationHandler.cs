using PetHelpers.Application.Abstractions;
using PetHelpers.Application.Dtos;
using PetHelpers.Application.Models;

namespace PetHelpers.Application.Volunteers.Queries.GetVolunteersWithPagination;

public class GetVolunteersWithPaginationHandler
    : IQueryHandler<PagedList<VolunteerDto>, GetFilteredVolunteersWithPaginationQuery>
{
    public Task<PagedList<VolunteerDto>> Handle(
        GetFilteredVolunteersWithPaginationQuery query,
        CancellationToken cancellationToken)
    {
        // TODO: сделать получение волонтеров с пагинацией и фильтрацией через Dapper
        // TODO: все, что не должно быть доступно в других сборках сделать internal, вместо public / пофиксить модификаторы доступа
    }
}

