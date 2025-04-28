using PetHelpers.Application.Abstractions;
using PetHelpers.Application.Dtos;

namespace PetHelpers.Application.Volunteers.Commands.UpdateSocialMedias;

public record UpdateVolunteerSocialMediasCommand(Guid VolunteerId, IEnumerable<SocialMediaDto> SocialMedias) : ICommand;