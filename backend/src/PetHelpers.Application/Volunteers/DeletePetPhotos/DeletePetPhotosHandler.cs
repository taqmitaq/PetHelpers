using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetHelpers.Application.Database;
using PetHelpers.Application.Dtos;
using PetHelpers.Application.Extensions;
using PetHelpers.Application.Providers;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Application.Volunteers.DeletePetPhotos;

public class DeletePetPhotosHandler
{
    private const string BUCKET_NAME = "photos";

    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IFileProvider _provider;
    private readonly ILogger<DeletePetPhotosHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly DeletePetPhotosCommandValidator _validator;

    public DeletePetPhotosHandler(
        IVolunteerRepository volunteerRepository,
        IFileProvider provider,
        ILogger<DeletePetPhotosHandler> logger,
        IUnitOfWork unitOfWork,
        DeletePetPhotosCommandValidator validator)
    {
        _volunteerRepository = volunteerRepository;
        _provider = provider;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<IReadOnlyList<FilePath>, ErrorList>> Handle(
        DeletePetPhotosCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var transaction = await _unitOfWork.BeginTransaction(cancellationToken);

        try
        {
            var petResult = await _volunteerRepository.GetPetById(command.PetId, cancellationToken);

            if (petResult.IsFailure)
            {
                return petResult.Error.ToErrorList();
            }

            var pet = petResult.Value;

            var fileDtos = command.PhotoDtos
                .Select(p => new FileDto(BUCKET_NAME, p.PathToStorage));

            var deleteResult = await _provider.DeleteFiles(fileDtos, cancellationToken);

            if (deleteResult.IsFailure)
            {
                return deleteResult.Error;
            }

            foreach (var filePath in deleteResult.Value)
            {
                var photoResult = Photo.Create(filePath.Path);

                if (photoResult.IsFailure)
                {
                    return photoResult.Error.ToErrorList();
                }

                pet.DeletePhoto(photoResult.Value);
            }

            await _unitOfWork.SaveChanges(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            _logger.LogInformation("Photos deleted from pet with id {petId}", pet.Id.Value);

            return deleteResult;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Can not delete photos from pet with {id}",
                command.PetId);

            await transaction.RollbackAsync(cancellationToken);

            return Error.Failure("file.delete", "Can not delete photos from pet")
                .ToErrorList();
        }
    }
}