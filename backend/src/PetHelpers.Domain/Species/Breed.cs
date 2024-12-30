using CSharpFunctionalExtensions;
using PetHelpers.Domain.Shared.Ids;
using PetHelpers.Domain.Shared.ValueObjects;

namespace PetHelpers.Domain.Species;

public sealed class Breed : Entity<BreedId>
{
    public Species Species { get; private set; }

    private Breed()
    {
    }

    public Title Title { get; private set; }

    public static Result<Breed, string> Create(Title title)
    {
        if (string.IsNullOrWhiteSpace(title.Text))
            return "Title cannot be empty.";

        var breed = new Breed { Title = title };

        return Result.Success<Breed, string>(breed);
    }
}