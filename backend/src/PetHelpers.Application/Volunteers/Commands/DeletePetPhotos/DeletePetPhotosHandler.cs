using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetHelpers.Application.Abstractions;
using PetHelpers.Application.Database;
using PetHelpers.Application.Extensions;
using PetHelpers.Application.Files;
using PetHelpers.Domain.Shared;
using FileInfo = PetHelpers.Application.Files.FileInfo;

namespace PetHelpers.Application.Volunteers.Commands.DeletePetPhotos;

public class DeletePetPhotosHandler : ICommandHandler<IReadOnlyList<FilePath>, DeletePetPhotosCommand>
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

            var fileInfos = command.PhotoDtos
                .Select(p => new FileInfo(FilePath.Create(p.PathToStorage).Value, BUCKET_NAME));

            var deleteResult = await _provider.DeleteFiles(fileInfos, cancellationToken);

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