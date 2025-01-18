using Bert.RateLimiters;

namespace PolygonApi.RateLimiting;

public class RollingWindowThrottlerHandler : DelegatingHandler
{
    private readonly RollingWindowThrottler _throttler;

    public RollingWindowThrottlerHandler(HttpMessageHandler innerHandler, int maxRequests, TimeSpan timeUnit)
        : base(innerHandler)
    {
        _throttler = new RollingWindowThrottler(maxRequests, timeUnit);
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        while (cancellationToken.IsCancellationRequested == false)
        {
            var shouldThrottle = _throttler.ShouldThrottle(1, out var waitTimeMillis);

            if (shouldThrottle)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(waitTimeMillis), cancellationToken);
            }
            else
            {
                break;
            }
        }

        cancellationToken.ThrowIfCancellationRequested();

        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
        {
            if (response.Headers.RetryAfter != null)
            {
                var retryAfter = response.Headers.RetryAfter.Delta ?? TimeSpan.FromSeconds(1);
                await Task.Delay(retryAfter, cancellationToken);
                return await base.SendAsync(request, cancellationToken);
            }
            else
            {
                // Default retry logic if no Retry-After header is present
                await Task.Delay(TimeSpan.FromSeconds(15), cancellationToken);
                return await base.SendAsync(request, cancellationToken);
            }
        }

        return response;
    }
}

