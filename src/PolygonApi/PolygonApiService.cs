using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using PolygonApi.Middleware;
using PolygonApi.Models.Financials;
using PolygonApi.RateLimiting;
using Refit;

namespace PolygonApi;

/// <summary>
/// Wrapper class for the Refit API.
/// </summary>
public class PolygonApiService : IDisposable
{
    private readonly ILogger _logging;

    private readonly Uri _baseUri;

    /// <summary>
    /// The underlying API surface.
    /// </summary>
    public IPolygonApi PolygonApi { get; private set; }

    private readonly HttpClient _authenticatedHttpClient;

    public PolygonApiService(PolygonApiAuth auth, bool useRateLimiter, ILogger logging)
    {
        var options = new JsonSerializerOptions()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        _baseUri = new Uri(auth.BaseUrl);

        //options.Converters.Add(new ISO8601DateTimeConverter()); //not needed
        //options.Converters.Add(new EscapeHtmlEncodedStringConverter());

        _logging = logging;

        if (useRateLimiter)
        {
            _authenticatedHttpClient = new HttpClient(
                new RollingWindowThrottlerHandler(new HttpClientDiagnosticsHandler(new AuthenticatedHttpClientHandler(auth.ApiKey), _logging),
                    5, TimeSpan.FromMinutes(1)) //polygon default 5 per minute
            )
            {
                BaseAddress = _baseUri
            };
        }
        else
        {
            _authenticatedHttpClient = new HttpClient(new HttpClientDiagnosticsHandler(new AuthenticatedHttpClientHandler(auth.ApiKey), _logging)) { BaseAddress = _baseUri };
        }

        PolygonApi = RestService.For<IPolygonApi>(_authenticatedHttpClient, new RefitSettings()
        {
            ContentSerializer = new SystemTextJsonContentSerializer(options)
        });
    }

    public async IAsyncEnumerable<FinancialResult> GetFinancials(string ticker)
    {
        string nextUrl = null;

        do
        {
            // Fetch the first or subsequent pages
            FinancialsResponse response;
            if (string.IsNullOrEmpty(nextUrl))
            {
                response = await PolygonApi.GetFinancialsAsync(ticker, 10);
            }
            else
            {
                // Use HttpClient for the next_url
                using var client = new HttpClient();
                var nextResponse = await client.GetStringAsync(nextUrl);
                response = JsonSerializer.Deserialize<FinancialsResponse>(nextResponse);
            }

            // Yield results
            foreach (var result in response.Results)
            {
                yield return result;
            }

            // Update the next URL
            nextUrl = response.NextUrl;
        } while (!string.IsNullOrEmpty(nextUrl));
    }
    
    public void Dispose()
    {
        _authenticatedHttpClient?.Dispose();
    }
}