//gs
using Day21.FeatureflagAPI.Models;
using Day21.FeatureflagAPI.Services;
using Day21.FeatureflagAPI.Services.Interfaces;


namespace Day21.FeatureflagAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFeatureServices( 
            this IServiceCollection services,
            IConfiguration  configuration
            )
        {
            services.Configure<FeatureOptions>(
            configuration.GetSection("Features"));

            services.AddScoped<IFeatureService, FeatureService>();

            return services;
        }
     

            
    }
}