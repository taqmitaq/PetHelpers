using Microsoft.Extensions.DependencyInjection;
using PetHelpers.Application.Species.CreateSpecies;

namespace PetHelpers.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateSpeciesHandler>();

        return services;
    }
}