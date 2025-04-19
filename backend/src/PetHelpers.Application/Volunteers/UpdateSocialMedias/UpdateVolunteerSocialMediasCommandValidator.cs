using FluentValidation;
using PetHelpers.Application.Validation;
using PetHelpers.Domain.Shared;
using PetHelpers.Domain.Volunteer.ValueObjects;

namespace PetHelpers.Application.Volunteers.UpdateSocialMedias;

public class UpdateVolunteerSocialMediasCommandValidator : AbstractValidator<UpdateVolunteerSocialMediasCommand>
{
    public UpdateVolunteerSocialMediasCommandValidator()
    {
        RuleFor(u => u.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());

        RuleFor(u => u.SocialMedias)
            .ForEach(r =>
            {
                r.Must(dto => Title.Create(dto.Title).IsSuccess)
                    .WithError(Errors.General.ValueIsInvalid("Title"));
                r.Must(dto => Link.Create(dto.Link).IsSuccess)
                    .WithError(Errors.General.ValueIsInvalid("Link"));
            });
    }
}