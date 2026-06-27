//gs
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Services;
using Day24.AttentionMeshOS.Storage;
using Day24.AttentionMeshOS.Options;
using System.Security.Cryptography.Xml;
using Microsoft.Extensions.Options;

namespace Day24.AttentionMeshOS.Extensions
{
    public static class ServiceCollecionEXtensions
    {
        public static IServiceCollection AddAttentionMesh(this 
            IServiceCollection services,
            IConfiguration configuration)
        {


            //===============================================================================
            //=========================//ConfigOptions//==========================
            //=========================================================================


            services.Configure<AttentionOptions>(configuration.GetSection("Attention"));
            //===================================================================================
            // bulk ingestion boudary setting and startup validation
            //=======================================================================================
            services.AddOptions<BulkIngestionOptions>()
                 .Bind(configuration.GetSection("BulkIngestion"))
                 .Validate(o => o.MaxInputCharacters > 0)
                 .Validate(o => o.MaxChunkCharacters > 0)
                 .Validate(o => o.MaxChunkCharacters <= o.MaxInputCharacters)
                 .Validate(o => o.ChunkOverlapCharacters >= 0)
                 .Validate(o => o.ChunkOverlapCharacters <= o.MaxChunkCharacters)
                 .Validate(o => o.MaxChunksPerRequest > 0)
                 .Validate(o => 
                    o.MaxInputCharacters / o.MaxChunkCharacters
                        <= o.MaxChunksPerRequest,
                        "Configured chunk limits can exceed the Maximum allowed  chunk count.")
                 .ValidateOnStart();


            //==============================================================================

            
            //InputProcessingPipeline config
            services.Configure<AttentionProcessingOptions>(configuration.GetSection("ProcessingPipeline"));
            services.Configure<AttentionInputValidationOptions>(configuration.GetSection("AttentionInputValidation"));
            services.Configure<TextNormalizationOptions>(configuration.GetSection("TextNormalization"));
            services.Configure<NoiseReductionOptions>(configuration.GetSection("NoiseReduction"));
            services.Configure<KeywordExtractionOptions>(configuration.GetSection("KeywordExtracion"));
            services.Configure<TagExtractionOptions>(configuration .GetSection("TagExtraction"));
            services.Configure<VectorPreparationOptions>(configuration.GetSection("VectorPreparation"));

            services.Configure<PostProcessingGuardOptions>(configuration.GetSection("PostProcessingGuard"));

            //sematics
            services.Configure<HyperVectorOptions>(configuration.GetSection("HyperVector"));
            services.Configure<ResonanceOptions>(configuration.GetSection("Resonance"));

            services.Configure<AttentionVelocityOptions>(configuration.GetSection("AttentionVelocity"));
            services.Configure<AttentionReleaseOptions>(configuration.GetSection("AttentionRelease"));

            //AMEAPATC
            //========
            services.Configure<CrystallizationOptions>(configuration.GetSection("Crystallization"));



            //=============================================================================================
            //============================//services//====================================================
                //==================================================================================
            //store
            services.AddSingleton<IAttentionStore, FileAttentionStore>();
            services.AddSingleton<IRawAttentionInputStore, FileRawAttentionInputStore>();
            services.AddSingleton<IAttentionBallMetadataStore, FileAttentionBallMetadataStore>();


            //Input Processor Pipeline abstractions and Services.
            services.AddSingleton<IChunkingService, ChunkingService>();
            services.AddSingleton<IBulkInputProcessor, BulkInputProcessor>();

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

            //semantics
            services.AddSingleton<IHyperVectorEncoder, HyperVectorEncoder>();
            services.AddSingleton<IAttentionBallMetadataFactory, AttentionBallMetadataFactory>();
            services.AddSingleton<IResonanceCalculator, ResonanceCalculator>();
            services.AddSingleton<IAttentionResonanceService, AttentionResonanceService>();

            //=================
            //AEMAPATC
            //---------
            services.AddSingleton<CrystallizationRuntime>(provider =>
            {
                var options = provider
                    .GetRequiredService<IOptions<CrystallizationOptions>>()
                    .Value;

                var birthStore = provider
                    .GetRequiredService<IDynamicTagBirthStore>();
                return new CrystallizationRuntime(
                    options,
                    birthStore);
            });

            services.AddSingleton<AttentionEnergyRouter>();
            services.AddSingleton<CentroidUpdater>();
            services.AddSingleton<SignedTernaryResonanceCalculator>();
            services.AddSingleton<SlotSelectionEngine>();
            services.AddSingleton<SignalVocabularyUpdater>();
            services.AddSingleton<DynamicTagNameBuilder>();
            services.AddSingleton<DynamicTagBirthFactory>();
            services.AddSingleton<IDynamicTagBirthStore, InMemoryDynamicTagBirthStore>();

            services.AddSingleton<ICrystallizationEngine, CrystallizationEngine>();
            services.AddSingleton<IInputProcessor,CrystallizationProcessor> ();
            //=====================
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
            
            services.AddSingleton<IAttentionVelocityService, AttentionVelocityService>();

            services.AddSingleton<IAttentionReleaseCandidateService, AttentionReleaseCandidateService>();
            services.AddSingleton<IAttentionReleaseService, AttentionReleaseService>();
            services.AddSingleton<IRawInputReleaseService, RawInputReleaseService>();
            return services;

        }
    }
}