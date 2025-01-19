using CSharpFunctionalExtensions;
using PetHelpers.Domain.Shared;
using PetHelpers.Domain.Shared.Ids;
using PetHelpers.Domain.Volunteer.Entities;

namespace PetHelpers.Application.Volunteers;

public interface IVolunteerRepository
{
    Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken);

    Task<Guid> Save(Volunteer volunteer, CancellationToken cancellationToken);

    Task<Result<Volunteer, Error>> GetById(
        VolunteerId volunteerId, CancellationToken cancellationToken);
}