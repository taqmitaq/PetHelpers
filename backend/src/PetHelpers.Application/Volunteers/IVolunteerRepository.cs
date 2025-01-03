using PetHelpers.Domain.Volunteer.Entities;

namespace PetHelpers.Application.Volunteers;

public interface IVolunteerRepository
{
    Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken);
}