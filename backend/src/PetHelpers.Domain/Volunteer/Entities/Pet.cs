using CSharpFunctionalExtensions;
using PetHelpers.Domain.Shared;
using PetHelpers.Domain.Shared.Ids;
using PetHelpers.Domain.Volunteer.ValueObjects;

namespace PetHelpers.Domain.Volunteer.Entities;

public sealed class Pet : Entity<PetId>
{
    private readonly List<Requisite> _requisites = [];

    public Volunteer Volunteer { get; private set; }

    private Pet()
        : base(PetId.NewId())
    {
    }

    public double Weight { get; private set; }

    public double Height { get; private set; }

    public bool IsCastrated { get; private set; }

    public bool IsVaccinated { get; private set; }

    public DateOnly BirthDate { get; private set; }

    public DateTime CreationDate { get; private set; }

    public PetName PetName { get; private set; }

    public Description Description { get; private set; }

    public Color Color { get; private set; }

    public HealthInfo HealthInfo { get; private set; }

    public Location Location { get; private set; }

    public Status HelpStatus { get; private set; }

    public SpeciesAndBreed SpeciesAndBreed { get; private set; }

    public PhoneNumber OwnersPhoneNumber { get; private set; }

    public IReadOnlyList<Requisite> Requisites => _requisites;

    public static Result<Pet, string> Create(
        double height,
        double weight,
        bool isCastrated,
        bool isVaccinated,
        DateOnly birthDate,
        PetName petName,
        Description description,
        Color color,
        HealthInfo healthInfo,
        Location location,
        Status helpStatus,
        SpeciesAndBreed speciesAndBreed,
        PhoneNumber ownersPhoneNumber)
    {
        var pet = new Pet
        {
            PetName = petName,
            Description = description,
            Color = color,
            HealthInfo = healthInfo,
            Location = location,
            Height = height,
            Weight = weight,
            IsCastrated = isCastrated,
            IsVaccinated = isVaccinated,
            BirthDate = birthDate,
            CreationDate = DateTime.UtcNow,
            HelpStatus = helpStatus,
            SpeciesAndBreed = speciesAndBreed,
            OwnersPhoneNumber = ownersPhoneNumber,
        };

        return Result.Success<Pet, string>(pet);
    }
}