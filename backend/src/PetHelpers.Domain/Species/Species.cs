﻿using CSharpFunctionalExtensions;
using PetHelpers.Domain.Shared;
using PetHelpers.Domain.Shared.Ids;
using PetHelpers.Domain.Species.Entities;

namespace PetHelpers.Domain.Species;

public sealed class Species : Entity<SpeciesId>
{
    private readonly List<Breed> _breeds = [];

    private bool _isDeleted = false;

    private Species()
        : base(SpeciesId.NewId())
    {
    }

    public Species(Title title)
        : base(SpeciesId.NewId()) => Title = title;

    public Title Title { get; private set; }

    public IReadOnlyList<Breed> Breeds => _breeds;

    public void Delete()
    {
        _isDeleted = true;

        foreach (var breed in _breeds)
            breed.Delete();
    }

    public void Restore()
    {
        _isDeleted = false;

        foreach (var breed in _breeds)
            breed.Restore();
    }

    public UnitResult<Error> UpdateTitle(Title title)
    {
        Title = title;

        return UnitResult.Success<Error>();
    }
}