using CSharpFunctionalExtensions;
using PetHelpers.Domain.Shared;
using PetHelpers.Domain.Shared.Ids;

namespace PetHelpers.Domain.Species.Entities;

public sealed class Breed : Entity<BreedId>
{
    private bool _isDeleted = false;

    public Species Species { get; private set; }

    private Breed()
        : base(BreedId.NewId())
    {
    }

    public Breed(Title title)
        : base(BreedId.NewId()) => Title = title;

    public Title Title { get; private set; }

    public void Delete() => _isDeleted = true;

    public void Restore() => _isDeleted = false;
}