using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetHelpers.Application.Species;
using PetHelpers.Domain.Shared;
using PetHelpers.Domain.Volunteer.Entities;
using PetHelpers.Domain.Volunteer.ValueObjects;

namespace PetHelpers.Application.Volunteers.AddPet;

public class AddPetHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly ILogger<AddPetHandler> _logger;

    public AddPetHandler(
        IVolunteerRepository volunteerRepository,
        ISpeciesRepository speciesRepository,
        ILogger<AddPetHandler> logger)
    {
        _volunteerRepository = volunteerRepository;
        _speciesRepository = speciesRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(
        AddPetRequest request,
        CancellationToken cancellationToken)
    {
        var speciesResult = await _speciesRepository.GetByTitle(request.Dto.Species, cancellationToken);

        if (speciesResult.IsFailure)
        {
            return Errors.General.ValueIsInvalid("Species");
        }

        var species = speciesResult.Value;

        var breeds = speciesResult.Value.Breeds;

        if (breeds.All(b => b.Title != request.Dto.Breed))
        {
            return Errors.General.ValueIsInvalid("Breed");
        }

        var breed = breeds.First(x => x.Title == request.Dto.Breed);

        var height = request.Dto.Height;

        var weight = request.Dto.Weight;

        var isCastrated = request.Dto.IsCastrated;

        var isVaccinated = request.Dto.IsVaccinated;

        var birthDate = request.Dto.BirthDate;

        var petName = PetName.Create(request.Dto.PetName).Value;

        var description = Description.Create(request.Dto.Description).Value;

        var color = Color.Create(request.Dto.Color).Value;

        var healthInfo = HealthInfo.Create(request.Dto.HealthInfo).Value;

        var (city, region, postalCode) = (
            request.Dto.Location.City,
            request.Dto.Location.Region,
            request.Dto.Location.PostalCode);

        var location = Location.Create(city, region, postalCode).Value;

        var helpStatus = Status.Create(request.Dto.HelpStatus).Value;

        var speciesAndBreed = new SpeciesAndBreed(species.Id, breed.Id);

        var ownersPhoneNumber = PhoneNumber.Create(request.Dto.OwnersPhoneNumber).Value;

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
                ownersPhoneNumber)
            .Value;

        var volunteerResult = await _volunteerRepository.GetById(request.VolunteerId, cancellationToken);

        if (volunteerResult.IsFailure)
            return Errors.General.ValueIsInvalid("VolunteerId");

        var volunteer = volunteerResult.Value;

        volunteer.AddPet(pet);

        var result = await _volunteerRepository.Save(volunteer, cancellationToken);

        _logger.LogInformation("Pet with id {petId} added to volunteer with id {VolunteerId}", pet.Id, volunteer.Id);

        return result;
    }
}