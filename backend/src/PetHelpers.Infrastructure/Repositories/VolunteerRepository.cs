using PetHelpers.Application.Volunteers;
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
}