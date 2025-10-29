namespace CodebridgeDogs.Middleware;

public class RateLimitingMiddleware
{
    private static readonly Dictionary<string, (int Count, DateTime Reset)> _requests = new();
    private readonly RequestDelegate _next;
    private readonly int _maxRequests;
    private readonly TimeSpan _period;

    public RateLimitingMiddleware(RequestDelegate next, int maxRequests = 10, int seconds = 1)
    {
        _next = next;
        _maxRequests = maxRequests;
        _period = TimeSpan.FromSeconds(seconds);
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var key = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        lock (_requests)
        {
            if (_requests.TryGetValue(key, out var entry))
            {
                if (entry.Reset > DateTime.UtcNow)
                {
                    if (entry.Count >= _maxRequests)
                    {
                        context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                        return;
                    }
                    _requests[key] = (entry.Count + 1, entry.Reset);
                }
                else
                {
                    _requests[key] = (1, DateTime.UtcNow + _period);
                }
            }
            else
            {
                _requests[key] = (1, DateTime.UtcNow + _period);
            }
        }

        await _next(context);
    }
}