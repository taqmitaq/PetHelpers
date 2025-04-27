using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetHelpers.Application.Database;
using PetHelpers.Application.Extensions;
using PetHelpers.Domain.Shared;
using PetHelpers.Domain.Volunteer.ValueObjects;

namespace PetHelpers.Application.Volunteers.UpdateRequisites;

public class UpdateVolunteerRequisitesHandler
{
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<UpdateVolunteerRequisitesHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UpdateVolunteerRequisitesCommandValidator _validator;

    public UpdateVolunteerRequisitesHandler(
        IVolunteerRepository repository,
        ILogger<UpdateVolunteerRequisitesHandler> logger,
        IUnitOfWork unitOfWork,
        UpdateVolunteerRequisitesCommandValidator validator)
    {
        _repository = repository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        UpdateVolunteerRequisitesCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var volunteerResult = await _repository.GetById(command.VolunteerId, cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var volunteer = volunteerResult.Value;

        var requisites = new List<Requisite>();

        foreach (var dto in command.Requisites)
        {
            var title = Title.Create(dto.Title).Value;

            var description = Description.Create(dto.Description).Value;

            requisites.Add(new Requisite(title, description));
        }

        volunteer.UpdateRequisites(requisites);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Updated requisites for volunteer with Id: {volunteer.Id}", volunteer.Id.Value);

        return volunteer.Id.Value;
    }
}