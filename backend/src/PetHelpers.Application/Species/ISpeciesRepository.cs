using CSharpFunctionalExtensions;
using PetHelpers.Domain.Shared;
using PetHelpers.Domain.Shared.Ids;

namespace PetHelpers.Application.Species;

public interface ISpeciesRepository
{
    Task<Guid> Add(Domain.Species.Entities.Species species, CancellationToken cancellationToken);

    Task<Guid> Save(Domain.Species.Entities.Species species, CancellationToken cancellationToken);

    Task<Guid> Delete(Domain.Species.Entities.Species species, CancellationToken cancellationToken);

    Task<Result<Domain.Species.Entities.Species, Error>> GetById(
        SpeciesId speciesId, CancellationToken cancellationToken);

    Task<Result<Domain.Species.Entities.Species, Error>> GetByTitle(
        Title title, CancellationToken cancellationToken);
}