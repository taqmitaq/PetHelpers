using Microsoft.Extensions.DependencyInjection;
using PetHelpers.Application.Species;
using PetHelpers.Infrastructure.Repositories;

namespace PetHelpers.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ApplicationDbContext>();
        services.AddScoped<ISpeciesRepository, SpeciesRepository>();

        return services;
    }
}