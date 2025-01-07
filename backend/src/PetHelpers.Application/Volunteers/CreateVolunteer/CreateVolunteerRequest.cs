﻿using PetHelpers.Application.Dtos;

namespace PetHelpers.Application.Volunteers.CreateVolunteer;

public record CreateVolunteerRequest(
    int YearsOfExperience,
    string Description,
    string Email,
    string PhoneNumber,
    FullNameDto FullName,
    IEnumerable<SocialMediaDto> SocialMedias,
    IEnumerable<RequisiteDto> Requisites);