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

            //InputProcessingPipeline config
            services.Configure<AttentionProcessingOptions>(configuration.GetSection("ProcessingPipeline"));
            services.Configure<AttentionInputValidationOptions>(configuration.GetSection("AttentionInputValidation"));
            services.Configure<TextNormalizationOptions>(configuration.GetSection("TextNormalization"));
            services.Configure<NoiseReductionOptions>(configuration.GetSection("NoiseReduction"));
            services.Configure<KeywordExtractionOptions>(configuration.GetSection("KeywordExtracion"));
            services.Configure<TagExtractionOptions>(configuration .GetSection("TagExtraction"));
            services.Configure<VectorPreparationOptions>(configuration.GetSection("VectorPreparation"));

            services.Configure<PostProcessingGuardOptions>(configuration.GetSection("PostProcessingGuard"));

            services.Configure<AttentionVelocityOptions>(configuration.GetSection("AttentionVelocity"));
            services.Configure<AttentionReleaseOptions>(configuration.GetSection("AttentionRelease"));

            //store
            services.AddSingleton<IAttentionStore, FileAttentionStore>();
            services.AddSingleton<IRawAttentionInputStore, FileRawAttentionInputStore>();

            //Input Processor Pipeline abstractions and Services.
            services.AddSingleton<IInputProcessor, InputValidationProcessor>();
            services.AddSingleton<IInputProcessingOrchestrator, InputProcessingOrchestrator>();
            services.AddSingleton<RawAttentionInputValidator>();
            services.AddSingleton<IInputProcessor, TextNormalizationProcessor>();
            services.AddSingleton<IInputProcessor, NoiseReductionProcessor>();
            services.AddSingleton<IInputProcessor, KeywordExtractionProcessor>();
            services.AddSingleton<IInputProcessor, TagExtractionProcessor>();
            services.AddSingleton<IInputProcessor, VectorPreparationProcessor>();

            services.AddSingleton<IInputProcessor, PostProcessingGuardProcessor>();
            services.AddSingleton<ITagRuleProvider, TagRuleProvider>();
            
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
            services.AddSingleton<IAttentionReleaseCandidateService, AttentionReleaseCandidateService>();
            return services;

        }
    }
}