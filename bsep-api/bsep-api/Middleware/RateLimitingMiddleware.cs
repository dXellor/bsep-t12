using bsep_bll.Dtos.Advertisements;
using Newtonsoft.Json;


namespace bsep_api.Middleware;

// all my will to live went here (https://youtu.be/doZE4Yqog5I?si=c17wZ6SNwJO-Fgga)
public class RateLimitingMiddleware
{
    private readonly RequestDelegate _next;

    private static readonly Dictionary<string, (int count, DateTime windowStart)> RateLimits = new();

    public RateLimitingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/api/advertisement/click"))
        {
            context.Request.EnableBuffering();
            var bodyStr = await new StreamReader(context.Request.Body).ReadToEndAsync();
            context.Request.Body.Position = 0;

            var clickRequest = JsonConvert.DeserializeObject<ClickRequestDto>(bodyStr);
            var email = clickRequest?.Email ?? "anonymous@gmail.com";
            var package = clickRequest?.Package ?? "Basic";

            int permitLimit = package switch
            {
                "Gold" => 10_000,
                "Standard" => 100,
                _ => 10
            };

            var key = email;
            if (!RateLimits.ContainsKey(key))
            {
                RateLimits[key] = (0, DateTime.UtcNow);
            }

            var (count, windowStart) = RateLimits[key];
            if (windowStart.AddMinutes(1) < DateTime.UtcNow)
            {
                windowStart = DateTime.UtcNow;
                count = 0;
            }

            if (count >= permitLimit)
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.Response.WriteAsync("Rate limit exceeded");
                return;
            }

            RateLimits[key] = (count + 1, windowStart);
        }

        await _next(context);
    }
}
