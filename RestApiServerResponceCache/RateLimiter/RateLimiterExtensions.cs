using System.Collections.Concurrent;

namespace RestApiServerResponceCache.RateLimiter
{
    public static class RateLimiterExtensions
    {
        public static IServiceCollection AddLimiter(this IServiceCollection services, int requestLimit, TimeSpan windowSize)
        {
            services.AddSingleton<RateLimiterMiddleWare>(sp => new RateLimiterMiddleWare(sp.GetService<RequestDelegate>()!, requestLimit, windowSize));
            return services;
        }
        public static IApplicationBuilder UseLimiter(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RateLimiterMiddleWare>();
        }

    }
}
