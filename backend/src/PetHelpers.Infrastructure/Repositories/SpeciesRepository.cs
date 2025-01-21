using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetHelpers.Application.Species;
using PetHelpers.Domain.Shared;
using PetHelpers.Domain.Shared.Ids;
using PetHelpers.Domain.Species.Entities;

namespace PetHelpers.Infrastructure.Repositories;

public class SpeciesRepository : ISpeciesRepository
{
    private readonly ApplicationDbContext _dbContext;

    public SpeciesRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Add(Species species, CancellationToken cancellationToken)
    {
        await _dbContext.Species.AddAsync(species, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return species.Id;
    }

    public async Task<Guid> Save(Species species, CancellationToken cancellationToken)
    {
        _dbContext.Species.Attach(species);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return species.Id;
    }

    public async Task<Result<Species, Error>> GetById(
        SpeciesId speciesId, CancellationToken cancellationToken)
    {
        var species = await _dbContext.Species
            .Include(s => s.Breeds)
            .FirstOrDefaultAsync(s => s.Id == speciesId, cancellationToken);

        if (species is null)
            return Errors.General.NotFound(speciesId);

        return species;
    }

    public async Task<Result<Species, Error>> GetByTitle(
        Title title, CancellationToken cancellationToken)
    {
        var species = await _dbContext.Species
            .Include(s => s.Breeds)
            .FirstOrDefaultAsync(s => s.Title == title, cancellationToken);

        if (species is null)
            return Errors.General.NotFound();

        return species;
    }
}