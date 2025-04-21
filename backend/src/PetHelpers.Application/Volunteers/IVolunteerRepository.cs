using CSharpFunctionalExtensions;
using PetHelpers.Domain.Shared;
using PetHelpers.Domain.Shared.Ids;
using PetHelpers.Domain.Volunteer;
using PetHelpers.Domain.Volunteer.Entities;

namespace PetHelpers.Application.Volunteers;

public interface IVolunteerRepository
{
    Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken);

    Guid Delete(Volunteer volunteer, CancellationToken cancellationToken);

    Task<Result<Volunteer, Error>> GetById(
        VolunteerId volunteerId, CancellationToken cancellationToken);

    Task<Result<Pet, Error>> GetPetById(
        PetId petId, CancellationToken cancellationToken);
}