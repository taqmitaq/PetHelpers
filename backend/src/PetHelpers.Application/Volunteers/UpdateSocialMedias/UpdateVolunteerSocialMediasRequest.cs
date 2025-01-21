using PetHelpers.Application.Dtos;

namespace PetHelpers.Application.Volunteers.UpdateSocialMedias;

public record UpdateVolunteerSocialMediasRequest(Guid VolunteerId, UpdateVolunteerSocialMediasDto Dto);