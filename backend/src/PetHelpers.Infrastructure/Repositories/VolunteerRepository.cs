using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetHelpers.Application.Volunteers;
using PetHelpers.Domain.Shared;
using PetHelpers.Domain.Shared.Ids;
using PetHelpers.Domain.Volunteer;
using PetHelpers.Domain.Volunteer.Entities;

namespace PetHelpers.Infrastructure.Repositories;

public class VolunteerRepository : IVolunteerRepository
{
    public ApplicationDbContext _dbContext { get; }

    public VolunteerRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken)
    {
        await _dbContext.Volunteers.AddAsync(volunteer, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return volunteer.Id;
    }

    public async Task<Guid> Save(Volunteer volunteer, CancellationToken cancellationToken)
    {
        _dbContext.Attach(volunteer);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return volunteer.Id;
    }

    public async Task<Guid> Delete(Volunteer volunteer, CancellationToken cancellationToken)
    {
        _dbContext.Volunteers.Remove(volunteer);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return volunteer.Id;
    }

    public async Task<Result<Volunteer, Error>> GetById(VolunteerId volunteerId, CancellationToken cancellationToken)
    {
        var volunteer = await _dbContext.Volunteers
            .Include(v => v.OwnedPets)
            .FirstOrDefaultAsync(v => v.Id == volunteerId, cancellationToken);

        if (volunteer is null)
            return Errors.General.NotFound(volunteerId);

        return volunteer;
    }

    public async Task<Result<Pet, Error>> GetPetById(PetId petId, CancellationToken cancellationToken)
    {
        var pet = await _dbContext.Volunteers
            .Include(v => v.OwnedPets)
            .Select(x => x.OwnedPets.FirstOrDefault(p => p.Id == petId))
            .FirstOrDefaultAsync(cancellationToken);

        if (pet is null)
            return Errors.General.NotFound(petId);

        return pet;
    }
}