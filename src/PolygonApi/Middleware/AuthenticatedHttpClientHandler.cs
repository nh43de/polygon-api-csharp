namespace PolygonApi.Middleware;

public class AuthenticatedHttpClientHandler : HttpClientHandler
{
    private readonly string _apiKey;
    private readonly bool _useAuthorizationHeader;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticatedHttpClientHandler"/> class.
    /// </summary>
    /// <param name="apiKey">Your Polygon.io API key.</param>
    /// <param name="useAuthorizationHeader">
    /// If true, the API key will be sent in the Authorization header as a Bearer token.
    /// If false, it will be included in the query string as "apiKey".
    /// </param>
    public AuthenticatedHttpClientHandler(string apiKey, bool useAuthorizationHeader = false)
    {
        _apiKey = apiKey;
        _useAuthorizationHeader = useAuthorizationHeader;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (_useAuthorizationHeader)
        {
            // Add the API key as a Bearer token in the Authorization header
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);
        }
        else //else Add the API key as a query string parameter
        {
            var uriBuilder = new UriBuilder(request.RequestUri);
            var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);
            query["apiKey"] = _apiKey;
            uriBuilder.Query = query.ToString();
            request.RequestUri = uriBuilder.Uri;
        }

        // Proceed with the request
        return await base.SendAsync(request, cancellationToken);
    }
}