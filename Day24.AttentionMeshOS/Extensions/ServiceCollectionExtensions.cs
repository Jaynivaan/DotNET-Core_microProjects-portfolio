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
            services.Configure<AttentionVelocityOptions>(configuration.GetSection("AttentionVelocity"));

            services.AddSingleton<IAttentionStore, FileAttentionStore>();
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
            services.AddSingleton<IAnchorStalenessService, AnchorStalenessService>();
            services.AddSingleton<IAnchorDemotionService, AnchorDemotionService>();
            services.AddSingleton<IAttentionReleaseService, AttentionReleaseService>();
            services.AddSingleton<IAttentionVelocityService, AttentionVelocityService>();

            return services;

        }
    }
}