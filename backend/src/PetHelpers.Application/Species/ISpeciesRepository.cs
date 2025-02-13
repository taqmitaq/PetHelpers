using CSharpFunctionalExtensions;
using PetHelpers.Domain.Shared;
using PetHelpers.Domain.Shared.Ids;

namespace PetHelpers.Application.Species;

public interface ISpeciesRepository
{
    Task<Guid> Add(Domain.Species.Species species, CancellationToken cancellationToken);

    Task<Guid> Save(Domain.Species.Species species, CancellationToken cancellationToken);

    Task<Guid> Delete(Domain.Species.Species species, CancellationToken cancellationToken);

    Task<Result<Domain.Species.Species, Error>> GetById(
        SpeciesId speciesId, CancellationToken cancellationToken);

    Task<Result<Domain.Species.Species, Error>> GetByTitle(
        Title title, CancellationToken cancellationToken);
}