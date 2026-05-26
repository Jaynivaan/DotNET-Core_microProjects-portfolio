//gs
using Day23.RealityFeedEngine.Services;
using Day23.RealityFeedEngine.Services.Interfaces;

namespace Day23.RealityFeedEngine.Extensions
{
    public static class ServiceCollecionExtensions
    {
        public static IServiceCollection  AddRealityFeedServices( this IServiceCollection services)
        {
            services.AddScoped<IFeedService, FeedService>();

            return services;
        }
    }
}