using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetHelpers.Application.Files.Delete;
using PetHelpers.Application.Files.Get;
using PetHelpers.Application.Files.Upload;
using PetHelpers.Application.Species.Create;
using PetHelpers.Application.Species.Delete;
using PetHelpers.Application.Species.Update;
using PetHelpers.Application.Volunteers.Create;
using PetHelpers.Application.Volunteers.Delete;
using PetHelpers.Application.Volunteers.UpdateMainInfo;
using PetHelpers.Application.Volunteers.UpdateRequisites;
using PetHelpers.Application.Volunteers.UpdateSocialMedias;

namespace PetHelpers.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateSpeciesHandler>();
        services.AddScoped<UpdateSpeciesHandler>();
        services.AddScoped<HardDeleteSpeciesHandler>();
        services.AddScoped<SoftDeleteSpeciesHandler>();

        services.AddScoped<CreateVolunteerHandler>();
        services.AddScoped<HardDeleteVolunteerHandler>();
        services.AddScoped<SoftDeleteVolunteerHandler>();
        services.AddScoped<UpdateVolunteerMainInfoHandler>();
        services.AddScoped<UpdateVolunteerRequisitesHandler>();
        services.AddScoped<UpdateVolunteerSocialMediasHandler>();

        services.AddScoped<DeleteFileHandler>();
        services.AddScoped<GetFileHandler>();
        services.AddScoped<UploadFileHandler>();

        services.AddValidatorsFromAssembly(typeof(CreateSpeciesHandler).Assembly);

        return services;
    }
}