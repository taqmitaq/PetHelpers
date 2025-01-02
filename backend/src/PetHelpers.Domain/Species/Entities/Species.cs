using CSharpFunctionalExtensions;
using PetHelpers.Domain.Shared;
using PetHelpers.Domain.Shared.Ids;

namespace PetHelpers.Domain.Species.Entities;

public sealed class Species : Entity<SpeciesId>
{
    private readonly List<Breed> _breeds = [];

    private Species()
        : base(SpeciesId.NewId())
    {
    }

    public Species(Title title)
        : base(SpeciesId.NewId()) => Title = title;

    public Title Title { get; private set; }

    public IReadOnlyList<Breed> Breeds => _breeds;
}