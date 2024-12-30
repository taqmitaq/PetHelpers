using CSharpFunctionalExtensions;

namespace PetHelpers.Domain.Volunteer.ValueObjects;

public class HealthInfo : ValueObject
{
    private HealthInfo(string info) => Info = info;

    public string Info { get; }

    public static Result<HealthInfo, string> Create(string info)
    {
        if (string.IsNullOrWhiteSpace(info))
            return "Health information cannot be empty.";

        var healthInfo = new HealthInfo(info);

        return Result.Success<HealthInfo, string>(healthInfo);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Info;
    }
}