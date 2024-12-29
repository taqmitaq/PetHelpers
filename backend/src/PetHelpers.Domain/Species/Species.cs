using CSharpFunctionalExtensions;
using PetHelpers.Domain.Shared.Ids;

namespace PetHelpers.Domain.Species;

public sealed class Species : Entity<SpeciesId>
{
    private readonly List<Breed> _breeds = [];

    private Species()
    {
    }

    public string Title { get; private set; }
    public IReadOnlyList<Breed> Breeds => _breeds;

    public static Result<Species, string> Create(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            return "Title cannot be empty.";

        var species = new Species { Title = title };

        return Result.Success<Species, string>(species);
    }
}