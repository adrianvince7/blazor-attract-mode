using Microsoft.Extensions.DependencyInjection;

namespace BlazorAttractMode;

/// <summary>
/// Extension methods for setting up AttractMode services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds AttractMode services to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configure">Optional configuration action.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddAttractMode(
        this IServiceCollection services,
        Action<AttractModeOptions>? configure = null)
    {
        if (configure != null)
        {
            services.Configure(configure);
        }
        else
        {
            services.Configure<AttractModeOptions>(options => { });
        }

        services.AddScoped<AttractModeService>();

        return services;
    }
}
