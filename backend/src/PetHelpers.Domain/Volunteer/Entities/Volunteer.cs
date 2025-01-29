using CSharpFunctionalExtensions;
using PetHelpers.Domain.Shared;
using PetHelpers.Domain.Shared.Ids;
using PetHelpers.Domain.Volunteer.ValueObjects;

namespace PetHelpers.Domain.Volunteer.Entities;

public sealed class Volunteer : Entity<VolunteerId>
{
    private readonly List<SocialMedia> _socialMedias = [];
    private readonly List<Requisite> _requisites = [];
    private readonly List<Pet> _ownedPets = [];

    private bool _isDeleted = false;

    private Volunteer()
        : base(VolunteerId.NewId())
    {
    }

    public int YearsOfExperience { get; private set; }

    public int PetsFoundHome { get; private set; }

    public int PetsLookingForHome { get; private set; }

    public int PetsInTreatment { get; private set; }

    public Description Description { get; private set; }

    public Email Email { get; private set; }

    public FullName FullName { get; private set; }

    public PhoneNumber PhoneNumber { get; private set; }

    public IReadOnlyList<SocialMedia> SocialMedias => _socialMedias;

    public IReadOnlyList<Requisite> Requisites => _requisites;

    public IReadOnlyList<Pet> OwnedPets => _ownedPets;

    public void Delete()
    {
        _isDeleted = true;

        foreach (var pet in _ownedPets)
            pet.Delete();
    }

    public void Restore()
    {
        _isDeleted = false;

        foreach (var pet in _ownedPets)
            pet.Restore();
    }

    public static Result<Volunteer, Error> Create(
        int yearsOfExperience,
        Description description,
        Email email,
        FullName fullName,
        PhoneNumber phoneNumber)
    {
        var volunteer = new Volunteer
        {
            YearsOfExperience = yearsOfExperience,
            Description = description,
            Email = email,
            FullName = fullName,
            PhoneNumber = phoneNumber,
        };

        return volunteer;
    }

    public UnitResult<Error> UpdateMainInfo(
        int yearsOfExperience,
        Description description,
        PhoneNumber phoneNumber,
        Email email,
        FullName fullName)
    {
        YearsOfExperience = yearsOfExperience;
        Description = description;
        PhoneNumber = phoneNumber;
        Email = email;
        FullName = fullName;

        return UnitResult.Success<Error>();
    }

    public UnitResult<Error> UpdateRequisites(IEnumerable<Requisite> requisites)
    {
        _requisites.Clear();
        _requisites.AddRange(requisites);

        return UnitResult.Success<Error>();
    }

    public UnitResult<Error> UpdateSocialMedias(IEnumerable<SocialMedia> socialMedias)
    {
        _socialMedias.Clear();
        _socialMedias.AddRange(socialMedias);

        return UnitResult.Success<Error>();
    }

    public UnitResult<Error> AddSocialMedia(SocialMedia socialMedia)
    {
        _socialMedias.Add(socialMedia);

        return UnitResult.Success<Error>();
    }

    public UnitResult<Error> AddRequisite(Requisite requisite)
    {
        _requisites.Add(requisite);

        return UnitResult.Success<Error>();
    }

    public UnitResult<Error> AddPet(Pet pet)
    {
        // Валидация
        _ownedPets.Add(pet);

        return UnitResult.Success<Error>();
    }
}