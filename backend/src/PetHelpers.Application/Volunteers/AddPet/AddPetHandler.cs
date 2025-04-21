using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetHelpers.Application.Database;
using PetHelpers.Application.Extensions;
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
    private readonly IUnitOfWork _unitOfWork;
    private readonly AddPetCommandValidator _validator;

    public AddPetHandler(
        IVolunteerRepository volunteerRepository,
        ISpeciesRepository speciesRepository,
        ILogger<AddPetHandler> logger,
        IUnitOfWork unitOfWork,
        AddPetCommandValidator validator)
    {
        _volunteerRepository = volunteerRepository;
        _speciesRepository = speciesRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        AddPetCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (validationResult.IsValid == false)
            return validationResult.ToList();

        var speciesResult = await _speciesRepository.GetById(command.SpeciesId, cancellationToken);

        if (speciesResult.IsFailure)
        {
            return speciesResult.Error.ToErrorList();
        }

        var species = speciesResult.Value;

        var breeds = species.Breeds;

        if (breeds.All(b => b.Id != command.BreedId))
        {
            return Errors.General.ValueIsInvalid("Breed").ToErrorList();
        }

        var breed = breeds.First(b => b.Id == command.BreedId);

        var height = command.Height;

        var weight = command.Weight;

        var isCastrated = command.IsCastrated;

        var isVaccinated = command.IsVaccinated;

        var birthDate = command.BirthDate;

        var petName = PetName.Create(command.PetName).Value;

        var description = Description.Create(command.Description).Value;

        var color = Color.Create(command.Color).Value;

        var healthInfo = HealthInfo.Create(command.HealthInfo).Value;

        var (city, region, postalCode) = (
            command.Location.City,
            command.Location.Region,
            command.Location.PostalCode);

        var location = Location.Create(city, region, postalCode).Value;

        var helpStatus = Status.Create(command.HelpStatus).Value;

        var speciesAndBreed = new SpeciesAndBreed(species.Id, breed.Id);

        var ownersPhoneNumber = PhoneNumber.Create(command.OwnersPhoneNumber).Value;

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

        var volunteerResult = await _volunteerRepository.GetById(command.VolunteerId, cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var volunteer = volunteerResult.Value;

        volunteer.AddPet(pet);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation(
            "Pet with id {petId} added to volunteer with id {VolunteerId}",
            pet.Id.Value,
            volunteer.Id.Value);

        return pet.Id.Value;
    }
}