using CSharpFunctionalExtensions;
using PetHelpers.Domain.Shared.Ids;
using PetHelpers.Domain.Shared.ValueObjects;

namespace PetHelpers.Domain.Species;

public sealed class Species : Entity<SpeciesId>
{
    private readonly List<Breed> _breeds = [];

    private Species()
    {
    }

    public Title Title { get; private set; }

    public IReadOnlyList<Breed> Breeds => _breeds;

    public static Result<Species, string> Create(Title title)
    {
        if (string.IsNullOrWhiteSpace(title.Text))
            return "Title cannot be empty.";

        var species = new Species { Title = title };

        return Result.Success<Species, string>(species);
    }
}