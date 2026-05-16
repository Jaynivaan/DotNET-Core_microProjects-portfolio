//gs
using Day16.MetaCognitiveAIGate.Options;
using Day16.MetaCognitiveAIGate.Services;
using Day16.MetaCognitiveAIGate.Services.Interfaces;
using Day16.MetaCognitiveAIGate.Services.Strategies;
using Day16.MetaCognitiveAIGate.Services.Validators;

namespace Day16.MetaCognitiveAIGate.Extensions
{
    //centralized dependency registrations
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection
            AddMetaCognitiveGate(
            this IServiceCollection services,
            IConfiguration configuration
            )
        {
            //options binding
            services.Configure<GateOptions>(
                configuration.GetSection("GateOptions")
                );

            //validators
            services.AddScoped
                <IPromptValidator, TokenLimitValidator>();

            //decision engine
            services.AddScoped
                <IGateDecisionService, GateDecisionService>();

            //routing strategies
            services.AddScoped
                <IRoutingStrategy, GeneralRoutingStrategy>();

            services.AddScoped
                <IRoutingStrategy, BackendRoutingStrategy>();

            return services; 

            //
        }
    }
}