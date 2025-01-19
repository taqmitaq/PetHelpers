using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetHelpers.Application.Species.Create;
using PetHelpers.Application.Species.Update;
using PetHelpers.Application.Volunteers.Create;
using PetHelpers.Application.Volunteers.UpdateMainInfo;

namespace PetHelpers.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateSpeciesHandler>();
        services.AddScoped<UpdateSpeciesHandler>();
        services.AddScoped<CreateVolunteerHandler>();
        services.AddScoped<UpdateVolunteerMainInfoHandler>();
        services.AddValidatorsFromAssembly(typeof(CreateSpeciesHandler).Assembly);

        return services;
    }
}