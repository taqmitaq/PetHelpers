using CSharpFunctionalExtensions;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Domain.Volunteer.ValueObjects;

public class HealthInfo : ValueObject
{
    private HealthInfo(string info) => Info = info;

    public string Info { get; }

    public static Result<HealthInfo, Error> Create(string info)
    {
        if (string.IsNullOrWhiteSpace(info))
            return Errors.General.ValueIsRequired(nameof(HealthInfo));

        var healthInfo = new HealthInfo(info);

        return healthInfo;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Info;
    }
}