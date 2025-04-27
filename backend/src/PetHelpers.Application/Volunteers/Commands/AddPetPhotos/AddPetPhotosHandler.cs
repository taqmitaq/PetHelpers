using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetHelpers.Application.Database;
using PetHelpers.Application.Extensions;
using PetHelpers.Application.Files;
using PetHelpers.Application.Messaging;
using PetHelpers.Domain.Shared;
using FileInfo = PetHelpers.Application.Files.FileInfo;

namespace PetHelpers.Application.Volunteers.AddPetPhotos;

public class AddPetPhotosHandler
{
    private const string BUCKET_NAME = "photos";

    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IFileProvider _provider;
    private readonly ILogger<AddPetPhotosHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMessageQueue<IEnumerable<FileInfo>> _messageQueue;
    private readonly AddPetPhotosCommandValidator _validator;

    public AddPetPhotosHandler(
        IVolunteerRepository volunteerRepository,
        IFileProvider provider,
        ILogger<AddPetPhotosHandler> logger,
        IUnitOfWork unitOfWork,
        AddPetPhotosCommandValidator validator,
        IMessageQueue<IEnumerable<FileInfo>> messageQueue)
    {
        _volunteerRepository = volunteerRepository;
        _provider = provider;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _messageQueue = messageQueue;
    }

    public async Task<Result<IReadOnlyList<FilePath>, ErrorList>> Handle(
        AddPetPhotosCommand command,
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

            List<FileData> files = [];

            foreach (var file in command.FileCommands)
            {
                var extension = Path.GetExtension(file.FileName);
                var filePath = FilePath.Create(Guid.NewGuid() + extension);

                if (filePath.IsFailure)
                    return filePath.Error.ToErrorList();

                var fileData = new FileData(file.Content, new FileInfo(filePath.Value, BUCKET_NAME));

                files.Add(fileData);
            }

            var uploadResult = await _provider.UploadFiles(files, cancellationToken);

            if (uploadResult.IsFailure)
            {
                await _messageQueue.WriteAsync(
                    files.Select(f => f.Info),
                    cancellationToken);

                return uploadResult.Error.ToErrorList();
            }

            foreach (var filePath in uploadResult.Value)
            {
                var photoResult = Photo.Create(filePath.Path);

                if (photoResult.IsFailure)
                {
                    return photoResult.Error.ToErrorList();
                }

                pet.AddPhoto(photoResult.Value);
            }

            await _unitOfWork.SaveChanges(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            _logger.LogInformation("Photos added to pet with id {petId}", pet.Id.Value);

            return Result.Success<IReadOnlyList<FilePath>, ErrorList>(uploadResult.Value);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Can not add photos to pet with {id}",
                command.PetId);

            await transaction.RollbackAsync(cancellationToken);

            return Error.Failure("file.upload", "Can not add photos to pet")
                .ToErrorList();
        }
    }
}