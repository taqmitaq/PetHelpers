using PetHelpers.API.Validation;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace PetHelpers.API;

public static class Inject
{
    public static IServiceCollection AddAPI(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddFluentValidationAutoValidation(configuration =>
        {
            configuration.OverrideDefaultResultFactoryWith<CustomResultFactory>();
        });

        return services;
    }
}