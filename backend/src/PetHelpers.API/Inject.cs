using Serilog;

namespace PetHelpers.API;

public static class Inject
{
    public static IServiceCollection AddAPI(this IServiceCollection services)
    {
        services.AddSerilog();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}