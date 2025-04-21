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
        addedPetResult.Value.Position.Should().Be(Position.First);
    }

    [Theory]
    [InlineData(5)]
    public void AddPet_WithExistingPets_ReturnsSuccessfulResult(int petsCount)
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
        var position = Position.Create(petsCount + 1).Value;

        result.IsSuccess.Should().BeTrue();
        addedPetResult.IsSuccess.Should().BeTrue();
        addedPetResult.Value.Position.Should().Be(position);
    }

    [Theory]
    [InlineData(1, 7, 10)]
    [InlineData(0, 9, 10)]
    [InlineData(0, 1, 2)]
    public void MovePet_Right_ReturnsCorrectResults(int petToMoveId, int targetPetId, int petsCount)
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

        var petToMove = volunteer.OwnedPets[petToMoveId];
        var targetPet = volunteer.OwnedPets[targetPetId];

        // Act
        var result = volunteer.MovePet(petToMove.Position, targetPet.Position);

        // Assert
        result.IsSuccess.Should().BeTrue();
        volunteer.OwnedPets[targetPetId].Should().Be(pets[petToMoveId]);

        for (int i = petToMoveId; i < targetPetId; i++)
        {
            volunteer.OwnedPets[i].Should().Be(pets[i + 1]);
        }

        for (int i = 0; i < pets.Count; i++)
        {
            volunteer.OwnedPets[i].Position.Value.Should().Be(i + 1);
        }
    }

    [Theory]
    [InlineData(7, 1, 10)]
    [InlineData(9, 0, 10)]
    [InlineData(1, 0, 2)]
    public void MovePet_Left_ReturnsCorrectResults(int petToMoveId, int targetPetId, int petsCount)
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

        var petToMove = volunteer.OwnedPets[petToMoveId];
        var targetPet = volunteer.OwnedPets[targetPetId];

        // Act
        var result = volunteer.MovePet(petToMove.Position, targetPet.Position);

        // Assert
        result.IsSuccess.Should().BeTrue();
        volunteer.OwnedPets[targetPetId].Should().Be(pets[petToMoveId]);

        for (int i = targetPetId + 1; i <= petToMoveId; i++)
        {
            volunteer.OwnedPets[i].Should().Be(pets[i - 1]);
        }

        for (int i = 0; i < pets.Count; i++)
        {
            volunteer.OwnedPets[i].Position.Value.Should().Be(i + 1);
        }
    }

    [Theory]
    [InlineData(10)]
    public void MovePet_WithIncorrectInput_ReturnsErrorResult(int petsCount)
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
        var incorrectPosition = Position.Create(pets.Count + 1).Value;

        // Act
        var incorrectTargetResult = volunteer.MovePet(petToMove.Position, incorrectPosition);
        var incorrectCurrentResult = volunteer.MovePet(incorrectPosition, petToMove.Position);

        // Assert
        incorrectTargetResult.IsFailure.Should().BeTrue();
        incorrectCurrentResult.IsFailure.Should().BeTrue();
    }
}