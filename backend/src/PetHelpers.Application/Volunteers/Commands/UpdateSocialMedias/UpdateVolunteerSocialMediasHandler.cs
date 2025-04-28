using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetHelpers.Application.Abstractions;
using PetHelpers.Application.Database;
using PetHelpers.Application.Extensions;
using PetHelpers.Domain.Shared;
using PetHelpers.Domain.Volunteer.ValueObjects;

namespace PetHelpers.Application.Volunteers.Commands.UpdateSocialMedias;

public class UpdateVolunteerSocialMediasHandler : ICommandHandler<Guid, UpdateVolunteerSocialMediasCommand>
{
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<UpdateVolunteerSocialMediasHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UpdateVolunteerSocialMediasCommandValidator _validator;

    public UpdateVolunteerSocialMediasHandler(
        IVolunteerRepository repository,
        ILogger<UpdateVolunteerSocialMediasHandler> logger,
        IUnitOfWork unitOfWork,
        UpdateVolunteerSocialMediasCommandValidator validator)
    {
        _repository = repository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        UpdateVolunteerSocialMediasCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var volunteerResult = await _repository.GetById(command.VolunteerId, cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var volunteer = volunteerResult.Value;

        var socialMedias = new List<SocialMedia>();

        foreach (var dto in command.SocialMedias)
        {
            var title = Title.Create(dto.Title).Value;

            var link = Link.Create(dto.Link).Value;

            socialMedias.Add(new SocialMedia(title, link));
        }

        volunteer.UpdateSocialMedias(socialMedias);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Updated social medias for volunteer with Id: {volunteer.Id}",
            volunteer.Id.Value);

        return volunteer.Id.Value;
    }
}