using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetHelpers.Application.Species.CreateSpecies;
using PetHelpers.Application.Volunteers.CreateVolunteer;

namespace PetHelpers.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateSpeciesHandler>();
        services.AddScoped<CreateVolunteerHandler>();
        services.AddValidatorsFromAssembly(typeof(CreateSpeciesHandler).Assembly);

        return services;
    }
}