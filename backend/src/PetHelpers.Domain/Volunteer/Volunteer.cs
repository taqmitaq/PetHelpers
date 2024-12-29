using CSharpFunctionalExtensions;
using PetHelpers.Domain.Shared.Ids;

namespace PetHelpers.Domain.Volunteer;

public sealed class Volunteer : Entity<VolunteerId>
{
    private readonly List<SocialMedia> _socialMedias = [];
    private readonly List<Requisite> _requisites = [];
    private readonly List<Pet> _ownedPets = [];

    private Volunteer()
    {
    }

    public int YearsOfExperience { get; private set; }
    public int PetsFoundHome { get; private set; }
    public int PetsLookingForHome { get; private set; }
    public int PetsInTreatment { get; private set; }
    public string Description { get; private set; }
    public string Email { get; private set; }
    public FullName FullName { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public IReadOnlyList<SocialMedia> SocialMedias => _socialMedias;
    public IReadOnlyList<Requisite> Requisites => _requisites;
    public IReadOnlyList<Pet> OwnedPets => _ownedPets;

    public static Result<Volunteer, string> Create(
        int yearsOfExperience,
        int petsFoundHome,
        int petsLookingForHome,
        int petsInTreatment,
        string description,
        string email,
        FullName fullName,
        PhoneNumber phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(description))
            return "Description cannot be empty.";

        if (string.IsNullOrWhiteSpace(email))
            return "Email cannot be empty.";

        var volunteer = new Volunteer
        {
            YearsOfExperience = yearsOfExperience,
            PetsFoundHome = petsFoundHome,
            PetsLookingForHome = petsLookingForHome,
            PetsInTreatment = petsInTreatment,
            Description = description,
            Email = email,
            FullName = fullName,
            PhoneNumber = phoneNumber,
        };

        return Result.Success<Volunteer, string>(volunteer);
    }
}