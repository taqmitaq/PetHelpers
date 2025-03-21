using CSharpFunctionalExtensions;

namespace PetHelpers.Domain.Shared;

public class Photo : ValueObject
{
    private Photo(string pathToStorage)
    {
        PathToStorage = pathToStorage;
    }

    public string PathToStorage { get; }

    public static Result<Photo, Error> Create(string pathToStorage)
    {
        if (string.IsNullOrWhiteSpace(pathToStorage))
            return Errors.General.ValueIsInvalid("PathToStorage");

        return new Photo(pathToStorage);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return PathToStorage;
    }
}