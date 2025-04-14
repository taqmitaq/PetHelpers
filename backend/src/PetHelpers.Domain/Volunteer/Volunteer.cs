using CSharpFunctionalExtensions;
using PetHelpers.Domain.Shared;
using PetHelpers.Domain.Shared.Ids;
using PetHelpers.Domain.Volunteer.Entities;
using PetHelpers.Domain.Volunteer.ValueObjects;

namespace PetHelpers.Domain.Volunteer;

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

    public int GetPetCount() => _ownedPets.Count;

    public Result<Pet, Error> GetPetById(PetId petId)
    {
        var pet = _ownedPets.FirstOrDefault(p => p.Id == petId);

        if (pet is null)
            return Errors.General.NotFound(petId.Value);

        return pet;
    }

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
        if (yearsOfExperience < 0)
            return Errors.General.ValueIsInvalid();

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
        var serialNumberResult = SerialNumber.Create(_ownedPets.Count + 1);

        if (serialNumberResult.IsFailure)
            return Errors.General.ValueIsInvalid("serial number");

        pet.SetSerialNumber(serialNumberResult.Value);

        _ownedPets.Add(pet);

        UpdateStatus(pet.HelpStatus);

        return UnitResult.Success<Error>();
    }

    public UnitResult<Error> MovePet(SerialNumber currentSerialNumber, SerialNumber targetSerialNumber)
    {
        int currentIndex = currentSerialNumber.Value - 1;
        int targetIndex = targetSerialNumber.Value - 1;

        if (currentIndex < 0 || currentIndex >= _ownedPets.Count)
            return Errors.General.ValueIsInvalid("current number");

        if (targetIndex < 0 || targetIndex >= _ownedPets.Count)
            return Errors.General.ValueIsInvalid("target number");

        return currentIndex > targetIndex
            ? MovePetLeft(currentIndex, targetIndex)
            : MovePetRight(currentIndex, targetIndex);
    }

    private UnitResult<Error> MovePetLeft(int currentIndex, int targetIndex)
    {
        if (currentIndex < targetIndex)
            throw new InvalidOperationException("Incorrect movement direction");

        while (currentIndex > targetIndex)
        {
            SwapPetsByIndex(currentIndex, currentIndex - 1);
            currentIndex--;
        }

        return UnitResult.Success<Error>();
    }

    private UnitResult<Error> MovePetRight(int currentIndex, int targetIndex)
    {
        if (currentIndex > targetIndex)
            throw new InvalidOperationException("Incorrect movement direction");

        while (currentIndex < targetIndex)
        {
            SwapPetsByIndex(currentIndex, currentIndex + 1);
            currentIndex++;
        }

        return UnitResult.Success<Error>();
    }

    private void SwapPetsByIndex(int first, int second)
    {
        (_ownedPets[first], _ownedPets[second]) = (_ownedPets[second], _ownedPets[first]);

        var temp = _ownedPets[first].SerialNumber;
        _ownedPets[first].SetSerialNumber(_ownedPets[second].SerialNumber);
        _ownedPets[second].SetSerialNumber(temp);
    }

    private int UpdateStatus(Status status)
    {
        if (status == Status.FoundHome)
            return PetsFoundHome = _ownedPets.Count(p => p.HelpStatus == status);

        if (status == Status.LookingForHome)
            return PetsLookingForHome = _ownedPets.Count(p => p.HelpStatus == status);

        if (status == Status.NeedsHelp)
            return PetsInTreatment = _ownedPets.Count(p => p.HelpStatus == status);

        throw new InvalidOperationException("Incorrect status");
    }
}