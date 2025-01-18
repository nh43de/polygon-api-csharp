using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace PolygonApi.Extensions;

/// <summary>
/// 
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add Polygon API client to the service container.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="apiKey"></param>
    /// <param name="useRateLimiter"></param>
    /// <param name="logger">Logger to use</param>
    /// <param name="baseUrl"></param>
    /// <returns></returns>
    public static IServiceCollection AddPolygonApiClient(this IServiceCollection services, string apiKey,
        bool useRateLimiter, ILogger logger, string baseUrl = "https://api.polygon.io")
    {
        if (useRateLimiter)
        {
            services.AddSingleton<PolygonApiService>(_ => new PolygonApiService(new PolygonApiAuth { ApiKey = apiKey, BaseUrl = baseUrl }, useRateLimiter, logger));
        }
        else
        {
            services.AddScoped<PolygonApiService>(_ => new PolygonApiService(new PolygonApiAuth { ApiKey = apiKey, BaseUrl = baseUrl }, useRateLimiter, logger));
        }

        return services;
    }
}