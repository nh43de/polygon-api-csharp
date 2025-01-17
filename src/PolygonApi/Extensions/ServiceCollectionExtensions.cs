using Microsoft.Extensions.DependencyInjection;

namespace PolygonApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPolygonApiClient(
        this IServiceCollection services,
        string baseUrl,
        string apiKey)
    {
        services.AddRefitClient<IPolygonApi>()
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            });

        return services;
    }
}