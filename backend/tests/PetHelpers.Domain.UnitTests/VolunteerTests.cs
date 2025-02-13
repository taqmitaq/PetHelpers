using FluentAssertions;
using PetHelpers.Domain.Shared;
using PetHelpers.Domain.Volunteer.Entities;
using PetHelpers.Domain.Volunteer.ValueObjects;

namespace PetHelpers.Domain.UnitTests;

public class VolunteerTests
{
    [Fact]
    public void AddPet_WithEmptyPets_ReturnsSuccessfulResult()
    {
        // Arrange
        var yearsOfExperience = 0;
        var description = Description.Create("test").Value;
        var email = Email.Create("test@test.com").Value;
        var fullName = FullName.Create("test", "test").Value;
        var phoneNumber = PhoneNumber.Create("+79000323565").Value;
        var volunteer = Volunteer.Volunteer.Create(
            yearsOfExperience,
            description,
            email,
            fullName,
            phoneNumber).Value;

        var height = 1;
        var weight = 1;
        var isCastrated = true;
        var isVaccinated = true;
        var birthDate = DateOnly.MinValue;
        var petName = PetName.Create("test").Value;
        var color = Color.Red;
        var healthInfo = HealthInfo.Create("test").Value;
        var location = Location.Create("test", "test", "test").Value;
        var helpStatus = Status.NeedsHelp;
        var speciesAndBreed = new SpeciesAndBreed(Guid.NewGuid(), Guid.NewGuid());
        var pet = Pet.Create(
            height,
            weight,
            isCastrated,
            isVaccinated,
            birthDate,
            petName,
            description,
            color,
            healthInfo,
            location,
            helpStatus,
            speciesAndBreed,
            phoneNumber).Value;

        // Act
        var result = volunteer.AddPet(pet);

        // Assert
        var addedPetResult = volunteer.GetPetById(pet.Id);

        result.IsSuccess.Should().BeTrue();
        addedPetResult.IsSuccess.Should().BeTrue();
        addedPetResult.Value.SerialNumber.Should().Be(SerialNumber.First);
    }

    [Fact]
    public void AddPet_WithExistingPets_ReturnsSuccessfulResult()
    {
        // Arrange
        const int petsCount = 5;

        var yearsOfExperience = 0;
        var description = Description.Create("test").Value;
        var email = Email.Create("test@test.com").Value;
        var fullName = FullName.Create("test", "test").Value;
        var phoneNumber = PhoneNumber.Create("+79000323565").Value;

        var volunteer = Volunteer.Volunteer.Create(
            yearsOfExperience,
            description,
            email,
            fullName,
            phoneNumber).Value;

        var height = 1;
        var weight = 1;
        var isCastrated = true;
        var isVaccinated = true;
        var birthDate = DateOnly.MinValue;
        var petName = PetName.Create("test").Value;
        var color = Color.Red;
        var healthInfo = HealthInfo.Create("test").Value;
        var location = Location.Create("test", "test", "test").Value;
        var helpStatus = Status.NeedsHelp;
        var speciesAndBreed = new SpeciesAndBreed(Guid.NewGuid(), Guid.NewGuid());

        var pets = Enumerable.Range(1, petsCount).Select(_ => Pet.Create(
            height,
            weight,
            isCastrated,
            isVaccinated,
            birthDate,
            petName,
            description,
            color,
            healthInfo,
            location,
            helpStatus,
            speciesAndBreed,
            phoneNumber).Value);

        foreach (var p in pets)
            volunteer.AddPet(p);

        var petToAdd = Pet.Create(
            height,
            weight,
            isCastrated,
            isVaccinated,
            birthDate,
            petName,
            description,
            color,
            healthInfo,
            location,
            helpStatus,
            speciesAndBreed,
            phoneNumber).Value;

        // Act
        var result = volunteer.AddPet(petToAdd);

        // Assert
        var addedPetResult = volunteer.GetPetById(petToAdd.Id);
        var serialNumber = SerialNumber.Create(petsCount + 1).Value;

        result.IsSuccess.Should().BeTrue();
        addedPetResult.IsSuccess.Should().BeTrue();
        addedPetResult.Value.SerialNumber.Should().Be(serialNumber);
    }

    [Fact]
    public void MovePet_Right_ReturnsCorrectResults()
    {
        // Arrange
        const int petsCount = 10;

        var yearsOfExperience = 0;
        var description = Description.Create("test").Value;
        var email = Email.Create("test@test.com").Value;
        var fullName = FullName.Create("test", "test").Value;
        var phoneNumber = PhoneNumber.Create("+79000323565").Value;

        var volunteer = Volunteer.Volunteer.Create(
            yearsOfExperience,
            description,
            email,
            fullName,
            phoneNumber).Value;

        var height = 1;
        var weight = 1;
        var isCastrated = true;
        var isVaccinated = true;
        var birthDate = DateOnly.MinValue;
        var petName = PetName.Create("test").Value;
        var color = Color.Red;
        var healthInfo = HealthInfo.Create("test").Value;
        var location = Location.Create("test", "test", "test").Value;
        var helpStatus = Status.NeedsHelp;
        var speciesAndBreed = new SpeciesAndBreed(Guid.NewGuid(), Guid.NewGuid());

        var pets = Enumerable.Range(1, petsCount).Select(_ => Pet.Create(
            height,
            weight,
            isCastrated,
            isVaccinated,
            birthDate,
            petName,
            description,
            color,
            healthInfo,
            location,
            helpStatus,
            speciesAndBreed,
            phoneNumber).Value).ToList();

        foreach (var p in pets)
            volunteer.AddPet(p);

        var petToMove1 = volunteer.OwnedPets[1];
        var petToMove2 = volunteer.OwnedPets[7];

        // Act
        var result = volunteer.MovePet(petToMove1.SerialNumber, petToMove2.SerialNumber);

        // Assert
        result.IsSuccess.Should().BeTrue();
        volunteer.OwnedPets[7].Should().Be(pets[1]);

        for (int i = 1; i < 7; i++)
        {
            volunteer.OwnedPets[i].Should().Be(pets[i + 1]);
        }

        for (int i = 0; i < pets.Count; i++)
        {
            volunteer.OwnedPets[i].SerialNumber.Value.Should().Be(i + 1);
        }
    }

    [Fact]
    public void MovePet_Left_ReturnsCorrectResults()
    {
        // Arrange
        const int petsCount = 10;

        var yearsOfExperience = 0;
        var description = Description.Create("test").Value;
        var email = Email.Create("test@test.com").Value;
        var fullName = FullName.Create("test", "test").Value;
        var phoneNumber = PhoneNumber.Create("+79000323565").Value;

        var volunteer = Volunteer.Volunteer.Create(
            yearsOfExperience,
            description,
            email,
            fullName,
            phoneNumber).Value;

        var height = 1;
        var weight = 1;
        var isCastrated = true;
        var isVaccinated = true;
        var birthDate = DateOnly.MinValue;
        var petName = PetName.Create("test").Value;
        var color = Color.Red;
        var healthInfo = HealthInfo.Create("test").Value;
        var location = Location.Create("test", "test", "test").Value;
        var helpStatus = Status.NeedsHelp;
        var speciesAndBreed = new SpeciesAndBreed(Guid.NewGuid(), Guid.NewGuid());

        var pets = Enumerable.Range(1, petsCount).Select(_ => Pet.Create(
            height,
            weight,
            isCastrated,
            isVaccinated,
            birthDate,
            petName,
            description,
            color,
            healthInfo,
            location,
            helpStatus,
            speciesAndBreed,
            phoneNumber).Value).ToList();

        foreach (var p in pets)
            volunteer.AddPet(p);

        var petToMove1 = volunteer.OwnedPets[7];
        var petToMove2 = volunteer.OwnedPets[1];

        // Act
        var result = volunteer.MovePet(petToMove1.SerialNumber, petToMove2.SerialNumber);

        // Assert
        result.IsSuccess.Should().BeTrue();
        volunteer.OwnedPets[1].Should().Be(pets[7]);

        for (int i = 2; i <= 7; i++)
        {
            volunteer.OwnedPets[i].Should().Be(pets[i - 1]);
        }

        for (int i = 0; i < pets.Count; i++)
        {
            volunteer.OwnedPets[i].SerialNumber.Value.Should().Be(i + 1);
        }
    }

    [Fact]
    public void MovePet_WithIncorrectInput_ReturnsErrorResult()
    {
        // Arrange
        const int petsCount = 10;

        var yearsOfExperience = 0;
        var description = Description.Create("test").Value;
        var email = Email.Create("test@test.com").Value;
        var fullName = FullName.Create("test", "test").Value;
        var phoneNumber = PhoneNumber.Create("+79000323565").Value;

        var volunteer = Volunteer.Volunteer.Create(
            yearsOfExperience,
            description,
            email,
            fullName,
            phoneNumber).Value;

        var height = 1;
        var weight = 1;
        var isCastrated = true;
        var isVaccinated = true;
        var birthDate = DateOnly.MinValue;
        var petName = PetName.Create("test").Value;
        var color = Color.Red;
        var healthInfo = HealthInfo.Create("test").Value;
        var location = Location.Create("test", "test", "test").Value;
        var helpStatus = Status.NeedsHelp;
        var speciesAndBreed = new SpeciesAndBreed(Guid.NewGuid(), Guid.NewGuid());

        var pets = Enumerable.Range(1, petsCount).Select(_ => Pet.Create(
            height,
            weight,
            isCastrated,
            isVaccinated,
            birthDate,
            petName,
            description,
            color,
            healthInfo,
            location,
            helpStatus,
            speciesAndBreed,
            phoneNumber).Value).ToList();

        foreach (var p in pets)
            volunteer.AddPet(p);

        var petToMove = volunteer.OwnedPets[0];
        var incorrectSerialNumber = SerialNumber.Create(pets.Count + 1).Value;

        // Act
        var incorrectTargetResult = volunteer.MovePet(petToMove.SerialNumber, incorrectSerialNumber);
        var incorrectCurrentResult = volunteer.MovePet(incorrectSerialNumber, petToMove.SerialNumber);

        // Assert
        incorrectTargetResult.IsFailure.Should().BeTrue();
        incorrectCurrentResult.IsFailure.Should().BeTrue();
    }
}