using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetHelpers.Domain.Shared;
using PetHelpers.Domain.Volunteer.ValueObjects;

namespace PetHelpers.Application.Volunteers.UpdateSocialMedias;

public class UpdateVolunteerSocialMediasHandler
{
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<UpdateVolunteerSocialMediasHandler> _logger;

    public UpdateVolunteerSocialMediasHandler(
        IVolunteerRepository repository,
        ILogger<UpdateVolunteerSocialMediasHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(
        UpdateVolunteerSocialMediasRequest request,
        CancellationToken cancellationToken)
    {
        var volunteerResult = await _repository.GetById(request.VolunteerId, cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error;

        var volunteer = volunteerResult.Value;

        var socialMedias = new List<SocialMedia>();

        foreach (var dto in request.Dto.SocialMedias)
        {
            var title = Title.Create(dto.Title).Value;

            var link = Link.Create(dto.Link).Value;

            socialMedias.Add(new SocialMedia(title, link));
        }

        volunteer.UpdateSocialMedias(socialMedias);

        var result = await _repository.Save(volunteer, cancellationToken);

        _logger.LogInformation("Updated social medias for volunteer with Id: {volunteer.Id}", result);

        return result;
    }
}