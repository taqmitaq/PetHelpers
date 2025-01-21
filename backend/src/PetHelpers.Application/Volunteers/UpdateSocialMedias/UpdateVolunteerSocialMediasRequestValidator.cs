using FluentValidation;
using PetHelpers.Application.Dtos;
using PetHelpers.Application.Validation;
using PetHelpers.Domain.Shared;
using PetHelpers.Domain.Volunteer.ValueObjects;

namespace PetHelpers.Application.Volunteers.UpdateSocialMedias;

public class UpdateVolunteerSocialMediasRequestValidator : AbstractValidator<UpdateVolunteerSocialMediasRequest>
{
    public UpdateVolunteerSocialMediasRequestValidator()
    {
        RuleFor(u => u.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}

public class UpdateVolunteerSocialMediasDtoValidator : AbstractValidator<UpdateVolunteerSocialMediasDto>
{
    public UpdateVolunteerSocialMediasDtoValidator()
    {
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