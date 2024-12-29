using CSharpFunctionalExtensions;
using PetHelpers.Domain.Shared.Ids;

namespace PetHelpers.Domain.Species;

public sealed class Breed : Entity<BreedId>
{
    public Species Species { get; private set; }

    private Breed()
    {
    }

    public string Title { get; private set; }

    public static Result<Breed, string> Create(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            return "Title cannot be empty.";

        var breed = new Breed { Title = title };

        return Result.Success<Breed, string>(breed);
    }
}