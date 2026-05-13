//gs
using Day13.LocalAiPingAPI.Interfaces;
using Day13.LocalAiPingAPI.Services;
using Day13.LocalAiPingAPI.Options;

namespace Day13.LocalAiPingAPI.Extensions
{

    //this file help to avoid program.cs from becoming a crowded registration room
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddLocalAiServices(
            this IServiceCollection services,
            IConfiguration configuration )

        {
            services.Configure<AiOptions>(
                configuration.GetSection("AiOptions"));

            services.AddScoped<IAiService, LocalAiService>();

            return services;

        }
    }
}