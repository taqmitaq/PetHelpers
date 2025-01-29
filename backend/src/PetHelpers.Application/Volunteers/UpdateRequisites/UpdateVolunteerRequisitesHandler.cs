using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetHelpers.Domain.Shared;
using PetHelpers.Domain.Volunteer.ValueObjects;

namespace PetHelpers.Application.Volunteers.UpdateRequisites;

public class UpdateVolunteerRequisitesHandler
{
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<UpdateVolunteerRequisitesHandler> _logger;

    public UpdateVolunteerRequisitesHandler(
        IVolunteerRepository repository,
        ILogger<UpdateVolunteerRequisitesHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(
        UpdateVolunteerRequisitesRequest request,
        CancellationToken cancellationToken)
    {
        var volunteerResult = await _repository.GetById(request.VolunteerId, cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error;

        var volunteer = volunteerResult.Value;

        var requisites = new List<Requisite>();

        foreach (var dto in request.Dto.Requisites)
        {
            var title = Title.Create(dto.Title).Value;

            var description = Description.Create(dto.Description).Value;

            requisites.Add(new Requisite(title, description));
        }

        volunteer.UpdateRequisites(requisites);

        var result = await _repository.Save(volunteer, cancellationToken);

        _logger.LogInformation("Updated requisites for volunteer with Id: {volunteer.Id}", result);

        return result;
    }
}