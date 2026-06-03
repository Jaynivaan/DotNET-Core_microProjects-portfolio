//gs
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Services;
using Day24.AttentionMeshOS.Storage;
using Day24.AttentionMeshOS.Options;

namespace Day24.AttentionMeshOS.Extensions
{
    public static class ServiceCollecionEXtensions
    {
        public static IServiceCollection AddAttentionMesh(this 
            IServiceCollection services,
            IConfiguration configuration)
        {

            services.Configure<AttentionOptions>(configuration.GetSection("Attention"));

            services.AddSingleton<IAttentionStore, InMemoryAttentionStore>();
            services.AddSingleton<ITextSignalClassifier, RuleBasedTextSignalClassifier>();
            services.AddSingleton<IPersistenceShotBuilder, PersistenceShotBuilder>();
            services.AddSingleton<IAttentionEngine, AttentionEngine>();
            services.AddSingleton<IAttentionMeshBuilder, AttentionMeshBuilder>();
            services.AddSingleton<ITextSimilarityService, TextSimilarityService>();            
            services.AddSingleton<IAttentionDecayService, AttentionDecayService>();
            services.AddSingleton<IAttentionReinforcementService, AttentionReinforcementService>();
            services.AddSingleton<IAttentionStateService, AttentionStateService>();
            services.AddSingleton<IAttentionAnchorService, AttentionAnchorService>();
            services.AddSingleton<IAnchorStateService, AnchorStateService>();
            services.AddSingleton<IAttentionPromotionService, AttentionPromotionService>();
            return services;

        }
    }
}