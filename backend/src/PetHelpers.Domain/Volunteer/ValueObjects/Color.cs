using CSharpFunctionalExtensions;
using PetHelpers.Domain.Shared.ValueObjects;

namespace PetHelpers.Domain.Volunteer.ValueObjects;

public class Color : ValueObject
{
    public static readonly Color Brown = new(nameof(Brown));
    public static readonly Color Red = new(nameof(Red));
    public static readonly Color Black = new(nameof(Black));
    public static readonly Color White = new(nameof(White));
    public static readonly Color Gold = new(nameof(Gold));
    public static readonly Color Apricot = new(nameof(Apricot));
    public static readonly Color Yellow = new(nameof(Yellow));
    public static readonly Color Cream = new(nameof(Cream));
    public static readonly Color Blue = new(nameof(Blue));
    public static readonly Color Grey = new(nameof(Grey));

    private static readonly Color[] _all = [Brown, Red, Black, White, Gold, Apricot, Yellow, Cream, Blue, Grey];

    private Color(string value) => Value = value;

    public string Value { get; }

    public static Result<Color, string> Create(string value)
    {
        value = value.Trim();

        if (_all.Any(c => string.Equals(value, c.Value, StringComparison.InvariantCultureIgnoreCase)))
        {
            var color = new Color(value);

            return Result.Success<Color, string>(color);
        }

        return "Color value is invalid";
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}