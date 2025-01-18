using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace PolygonApi.Middleware;

/// <summary>
/// Just a middleware that will help log requests if needed.
/// </summary>
public class HttpClientDiagnosticsHandler : DelegatingHandler
{
    private readonly ILogger Log;

    public HttpClientDiagnosticsHandler(HttpMessageHandler innerHandler, ILogger log) : base(innerHandler)
    {
        Log = log;
    }

    public HttpClientDiagnosticsHandler()
    {
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var totalElapsedTime = Stopwatch.StartNew();

        Log.LogDebug(string.Format("Request: {0}", request));
        if (request.Content != null)
        {
            var content = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
            Log.LogDebug(string.Format("Request Content: {0}", content));
        }

        var responseElapsedTime = Stopwatch.StartNew();
        var response = await base.SendAsync(request, cancellationToken);

        Log.LogDebug(string.Format("Response: {0}", response));
        if (response.Content != null)
        {
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            Log.LogDebug(string.Format("Response Content: {0}", content));
        }

        responseElapsedTime.Stop();
        Log.LogDebug(string.Format("Response elapsed time: {0} ms", responseElapsedTime.ElapsedMilliseconds));

        totalElapsedTime.Stop();
        Log.LogDebug(string.Format("Total elapsed time: {0} ms", totalElapsedTime.ElapsedMilliseconds));

        return response;
    }
}
