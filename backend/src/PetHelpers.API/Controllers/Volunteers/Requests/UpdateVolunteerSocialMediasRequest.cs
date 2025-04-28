using PetHelpers.Application.Dtos;
using PetHelpers.Application.Volunteers.Commands.UpdateSocialMedias;

namespace PetHelpers.API.Controllers.Volunteers.Requests;

public record UpdateVolunteerSocialMediasRequest(IEnumerable<SocialMediaDto> SocialMedias)
{
    public UpdateVolunteerSocialMediasCommand ToCommand(Guid id) => new(id, SocialMedias);
}