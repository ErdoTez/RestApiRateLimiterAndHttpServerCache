using System.Collections.Concurrent;

namespace RestApiServerResponceCache.RateLimiter
{
    public class RateLimiterMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly int _requestCounts;
        private readonly int _requestLimit;
        private readonly TimeSpan _windowSize;

        public RateLimiterMiddleWare(RequestDelegate next, int requestLimit, TimeSpan windowSize)
        {
            _next = next;
            _requestCounts = 0;
            _requestLimit = requestLimit;
            _windowSize = windowSize;
        }

        public async Task Invoke(HttpContext context)
        {
            string clientIp = context.Connection!.RemoteIpAddress!.ToString();
            string requestPath = context.Request.Path.ToString();

            string requestKey = $"{clientIp}:{requestPath}";

            int requestCount = _requestCounts + 1;

            if (requestCount > _requestLimit)
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.Response.WriteAsync("Too many requests. Please try again later.");
                return;
            }
            await Task.Delay(_windowSize);
            await _next(context);
        }
    }
}
