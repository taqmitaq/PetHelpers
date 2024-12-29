using CSharpFunctionalExtensions;
using PetHelpers.Domain.Shared.Ids;

namespace PetHelpers.Domain.Volunteer;

public sealed class Pet : Entity<PetId>
{
    private readonly List<Requisite> _requisites = [];

    public Volunteer Volunteer { get; private set; }

    private Pet()
    {
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Color { get; private set; }
    public string HealthInfo { get; private set; }
    public string Location { get; private set; }
    public double Weight { get; private set; }
    public double Height { get; private set; }
    public bool IsCastrated { get; private set; }
    public bool IsVaccinated { get; private set; }
    public DateOnly BirthDate { get; private set; }
    public DateTime CreationDate { get; private set; }
    public Status HelpStatus { get; private set; }
    public SpeciesAndBreed SpeciesAndBreed { get; private set; }
    public PhoneNumber OwnersPhoneNumber { get; private set; }
    public IReadOnlyList<Requisite> Requisites => _requisites;

    public static Result<Pet, string> Create(
        string name,
        string description,
        string color,
        string healthInfo,
        string location,
        double height,
        double weight,
        bool isCastrated,
        bool isVaccinated,
        DateOnly birthdate,
        Status helpStatus,
        SpeciesAndBreed speciesAndBreed,
        PhoneNumber ownersPhoneNumber)
    {
        if (string.IsNullOrWhiteSpace(name))
            return "Name cannot be empty.";

        if (string.IsNullOrWhiteSpace(description))
            return "description cannot be empty.";

        if (string.IsNullOrWhiteSpace(color))
            return "color cannot be empty.";

        if (string.IsNullOrWhiteSpace(healthInfo))
            return "healthInfo cannot be empty.";

        if (string.IsNullOrWhiteSpace(location))
            return "location cannot be empty.";

        var pet = new Pet
        {
            Name = name,
            Description = description,
            Color = color,
            HealthInfo = healthInfo,
            Location = location,
            Height = height,
            Weight = weight,
            IsCastrated = isCastrated,
            IsVaccinated = isVaccinated,
            BirthDate = birthdate,
            CreationDate = DateTime.UtcNow,
            HelpStatus = helpStatus,
            SpeciesAndBreed = speciesAndBreed,
            OwnersPhoneNumber = ownersPhoneNumber,
        };

        return Result.Success<Pet, string>(pet);
    }
}