using PetHelpers.Application.Volunteers.Queries.GetVolunteersWithPagination;

namespace PetHelpers.API.Controllers.Volunteers.Requests;

public record GetVolunteersWithPaginationRequest(
    string? Name,
    int? PositionFrom,
    int? PositionTo,
    string? SortBy,
    string? SortDirection,
    int Page,
    int PageSize)
{
    public GetFilteredVolunteersWithPaginationQuery ToQuery() =>
        new(Name, PositionFrom, PositionTo, SortBy, SortDirection, Page, PageSize);
}