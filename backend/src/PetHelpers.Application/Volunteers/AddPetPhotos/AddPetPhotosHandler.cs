using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetHelpers.Application.Providers;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Application.Volunteers.AddPetPhotos;

public class AddPetPhotosHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IFileProvider _provider;
    private readonly ILogger<AddPetPhotosHandler> _logger;


    public AddPetPhotosHandler(
        IVolunteerRepository volunteerRepository,
        IFileProvider provider,
        ILogger<AddPetPhotosHandler> logger)
    {
        _volunteerRepository = volunteerRepository;
        _provider = provider;
        _logger = logger;
    }

    public async Task<Result<string, Error>> Handle(
        AddPetPhotosRequest request,
        CancellationToken cancellationToken)
    {
        var petResult = await _volunteerRepository.GetPetById(request.PetId, cancellationToken);

        if (petResult.IsFailure)
        {
            return petResult.Error;
        }

        var pet = petResult.Value;

        var uploadResult = await _provider.Upload(request.FileData, cancellationToken);

        if (uploadResult.IsFailure)
        {
            return uploadResult.Error;
        }

        var photoResult = Photo.Create(uploadResult.Value);

        if (photoResult.IsFailure)
        {
            return photoResult.Error;
        }

        pet.AddPhoto(photoResult.Value);

        _ = await _volunteerRepository.Save(pet.Volunteer, cancellationToken);

        _logger.LogInformation("Photos added to pet with id {petId}", pet.Id);

        return request.FileData.ObjectName;
    }
}