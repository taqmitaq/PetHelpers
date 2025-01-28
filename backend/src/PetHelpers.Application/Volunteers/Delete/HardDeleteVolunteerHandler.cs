using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetHelpers.Domain.Shared;

namespace PetHelpers.Application.Volunteers.Delete;

public class HardDeleteVolunteerHandler
{
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<HardDeleteVolunteerHandler> _logger;

    public HardDeleteVolunteerHandler(
        IVolunteerRepository repository,
        ILogger<HardDeleteVolunteerHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(
        DeleteVolunteerRequest request,
        CancellationToken cancellationToken)
    {
        var volunteerResult = await _repository.GetById(request.VolunteerId, cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error;

        var volunteer = volunteerResult.Value;

        var result = await _repository.Delete(volunteer, cancellationToken);

        _logger.LogInformation("Deleted volunteer with id: {volunteerId}", result);

        return result;
    }
}