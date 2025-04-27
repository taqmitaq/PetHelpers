using PetHelpers.Application.Dtos;

namespace PetHelpers.Application.Volunteers.UpdateSocialMedias;

public record UpdateVolunteerSocialMediasCommand(Guid VolunteerId, IEnumerable<SocialMediaDto> SocialMedias);