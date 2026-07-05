//gs
using System;
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
            //============================================================================
            //AEMAPATC-options
            //===========================================================================
            services
                .AddOptions<CrystallizationOptions>()
                .Bind(configuration.GetSection("Crystallization"))
                .ValidateDataAnnotations()
                .Validate(
                    options => options.ColdThreshold < options.WarmThreshold,
                    "AEM-APATC configuration error: ColdThreshold must be less than WarmThreshold.")
                .Validate(
                    options => options.WarmPromotionCount < options.HotPromotionCount,
                    "AEM-APATC configuration error: WarmPromotionCount must be less than HotPromotionCount.")
                .ValidateOnStart();
            //==============================================================================
            //AEM-ESGF-options
            //=============================================================================
            services
                .AddOptions<GravityOptions>()
                .Bind(configuration.GetSection("Gravity"))
                .ValidateDataAnnotations()
                .Validate(
                    options => options.FieldFormationThreshold <= options.MergeThreshold,
                    "AEM-ESGF configuration error: FieldFormationThreshold must be less than or equal to MergeThreshold.")
                .Validate(
                    options => options.ResonanceThreshold <= options.MergeThreshold,
                    "AEM-ESGF configuration error: ResonanceThreshold must be less than or equal to MergeThreshold.")
                .Validate(
                    options => Math.Abs (
                        options.SignedTernaryWeight + options.VocabularyWeight - 1.0f) < 0.001f,
                    "AEM-ESGF configuration error: SignedTernaryWeight and VocabularyWeight must be total 1.0.")
                .Validate(
                    options => options.MaxSemanticMass >= options.BaseParticipationMass,
                    "AEM-ESGF configuration error: MaxSemanticMass must be greater than or equal to BaseParticipationMass.")
                .ValidateOnStart();


            //==============================================================================
            //AEM-SPF-options
            //=============================================================================
            services
                .AddOptions<SemanticPhysicsOptions>()
                .Bind(configuration.GetSection("SemanticPhysics"))
                .ValidateDataAnnotations()
                .Validate(
                    options => options.MinimumEnergy <= options.MaximumEnergy,
                    "AEM-SPF configuration error: MinimumEnergy must be less than or equal to MaximumEnergy.")
                .Validate(
                    options => options.MinimumStability <= options.MaximumStability,
                    "AEM-SPF configuration error: MinimumStability must be less than or equal to MaximumStability.")
                .Validate(
                    options => options.MinimumRadius <= options.MaximumRadius,
                    "AEM-SPF configuration error : MinimumRadius must be less than or equal to MaximumRadius")
                .Validate(
                    options => options.MomentumSensitivity > 0f,
                    "AEM-SPF configuration error: Momentum Sensitivity must be greater than zero.")
                .Validate(
                    options => options.PotentialRadiusPenalty >= 0f,
                    "AEM-SPF configuration error : PotentialRadiusPenalty must be non-negative.")
                .Validate(
                    options => 
                        options.PotentialMassWeight >= 0f &&
                        options.PotentialEnergyWeight >= 0f && 
                        options.PotentialStabilityWeight >= 0f ,
                    "AEM-SPF configuration error: Potential weights must be non-negative.")
                .Validate(
                    options =>
                        options.PotentialMassWeight > 0f||
                        options.PotentialEnergyWeight > 0f ||
                        options.PotentialStabilityWeight > 0f,
                    "AEM-SPF configuration error: At least one Potential weight must be greater than zero.")

                .ValidateOnStart();

            //===============================================================================
            //Persistence-options
            //===============================================================================
            services
                .AddOptions<PersistenceOptions>()
                .Bind(configuration.GetSection("Persistence"))
                .ValidateDataAnnotations()
                .Validate(
                    options => !string.IsNullOrWhiteSpace(options.DataDirectory),
                    "Persistence configuration error: DataDirectory must not be empty.")
                .Validate(
                    options => !string.IsNullOrWhiteSpace(options.SaveFileName),
                    "Persistence configuration error: SaveFileName must not be empty.")
                .Validate(
                    options => options.FormatVersion > 0,
                    "Persistence configuration error: FormatVersion Must be greater than zero.")
                .Validate(
                    options => options.SignatureLength > 0,
                    "Persistence configuration error: signature Length must be greater than zero.")
                .ValidateOnStart();

            //==========================================================================
            //Candidate-ResolutionOptins
            //===========================================================================
            services
                .AddOptions<CandidateResolutionOptions>()
                .Bind(configuration.GetSection("CandidateResolution"))
                .ValidateDataAnnotations()
                .Validate(
                    options => !string.IsNullOrWhiteSpace(options.ResolverType),
                    "CandidateResolution configuration error: ResolverType must not be empty.")
                .Validate(
                    options =>
                        string.Equals(options.ResolverType, "AllFields", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(options.ResolverType, "Fingerprint", StringComparison.OrdinalIgnoreCase),
                    "Candidate Resolution Configuration error: Resolver Type must be either AllFields or Fingerprint.")
                .Validate(
                    options => options.FingerprintBlockSize > 0,
                    "Candidate Resolution configuration error: FingerprintBlockSize must be greater than 0.")
                .Validate(
                    options => options.MinimumCandidateCount >= 0,
                    "Candidate Resolution configuration error: Minimum Candidate count must be greater than or equal to 0.")
                .Validate(
                    options => options.MaximumCandidateCount > 0,
                    "Candidate Resolution configuration error: Maximum Candidate Count must be greater than  0.")
                .Validate(
                    options => options.MaximumCandidateCount >= options.MinimumCandidateCount,
                    "CandidateResolution configuration error : MaximumCandidateCount must be greater than or equal to minimum candidateCount.")
                .ValidateOnStart();

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

            services.AddSingleton<IInputProcessor, CrystallizationProcessor>();
            services.AddSingleton<IInputProcessor, GravityFormationProcessor>();

            services.AddSingleton<IInputProcessor, PostProcessingGuardProcessor>();
            services.AddSingleton<ITagRuleProvider, TagRuleProvider>();

            //semantics
            services.AddSingleton<IHyperVectorEncoder, HyperVectorEncoder>();
            services.AddSingleton<IAttentionBallMetadataFactory, AttentionBallMetadataFactory>();
            services.AddSingleton<IResonanceCalculator, ResonanceCalculator>();
            services.AddSingleton<IAttentionResonanceService, AttentionResonanceService>();

            //========================================================================================
            //AEMAPATC- services registrations
            //--------------------------------------------------------------------------------------
            services.AddSingleton<CrystallizationRuntime>(provider =>
            {
                var options = provider
                    .GetRequiredService<IOptions<CrystallizationOptions>>()
                    .Value;

                var birthStore = provider
                    .GetRequiredService<IDynamicTagRegistry>();
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
            services.AddSingleton<IDynamicTagRegistry, InMemoryDynamicTagRegistry>();

            services.AddSingleton<ICrystallizationEngine, CrystallizationEngine>();
            


            //==============================================
            //Aem-Apatc Health vitals
            //================================================================
            services.AddSingleton<IRuntimeSnapshotProvider, RuntimeSnapshotProvider>();
            services.AddSingleton<IRuntimeHealthProvider, RuntimeHealthProvider> ();
            services.AddSingleton<IRuntimeStatisticsProvider, RuntimeStatisticsProvider> ();
            services.AddSingleton<IPerformanceBenchmarkProvider, PerformanceBenchmarkProvider>();

            //===================================================================
            //AEM-ESGF- service registrations
            //====================================================================
            services.AddSingleton<IGravityRuntime, GravityRuntime>();
            services.AddSingleton<IGravityRegistry, GravityRegistry>();
            services.AddSingleton<GravityFieldSelectionEngine>();
            services.AddSingleton<GravityProximityCalculator>();
            services.AddSingleton<GravityMembershipManager>();
            services.AddSingleton<GravityFieldSignatureUpdater>();
            services.AddSingleton<GravityFieldFactory>();
            services.AddSingleton<IGravityFormationEngine, GravityFormationEngine>();
            services.AddSingleton<ISemanticMassEngine, SemanticMassEngine>();
            services.AddSingleton<IGravityLifecycleManager, GravityLifecycleManager>();
            services.AddSingleton<ParticipationMetricsProvider>();
            services.AddSingleton<GravityRuntimeAggregator>();
            services.AddSingleton<IGravitySnapshotProvider, GravitySnapshotProvider>();
            services.AddSingleton<IGravityStatisticsProvider, GravityStatisticsProvider>();
            services.AddSingleton<IGravityHealthProvider, GravityHealthProvider>();

            //==============================================================================
            //AEM-SPF service registrations
            //=============================================================================
            services.AddSingleton<ISemanticPhysicsFramework, SemanticPhysicsFramework>();

            services.AddSingleton<ISemanticPhysicsLaw, AttentionEnergyLaw>();
            services.AddSingleton<ISemanticPhysicsLaw, StabilityLaw>();
            services.AddSingleton<ISemanticPhysicsLaw, RadiusLaw>();
            services.AddSingleton<ISemanticPhysicsLaw, AttractionPotentialLaw>();
            services.AddSingleton<ISemanticPhysicsLaw, SemanticMomentumLaw>();

            //---------------------------------------------------------------------------------
            //Persistence registrations
            //=====================================================================
            services.AddSingleton<IPersistenceValidator, PersistenceValidator>();
            services.AddSingleton<IAttentionMeshSaveStore, JsonAttentionMeshSaveStore>();

            services.AddSingleton<IDynamicTagPersistenceSerializer, DynamicTagPersistenceSerializer>();
            services.AddSingleton<IGravityRegistryPersistenceSerializer, GravityRegistryPersistenceSerializer>();
            services.AddSingleton<IGravityRuntimePersistenceSerializer, GravityRuntimePersistenceSerializer>();
            services.AddSingleton<ISemanticPhysicsPersistenceSerializer, SemanticPhysicsPersistenceSerializer>();

            services.AddSingleton<PersistenceValidationHarness>();

            services.AddSingleton<IPersistenceCoordinator, PersistenceCoordinator>();

            //=====================
            //CandidateResolution
            //--------
            services.AddSingleton<ICandidateResolver, AllFieldsCandidateResolver>();
            services.AddSingleton<ICandidateResolver, FingerprintCandidateResolver>();
            services.AddSingleton<CandidateFingerprintBuilder>();
            services.AddSingleton<GravityFieldCandidateFingerprintProvider>();
            services.AddSingleton<CandidateOrderingService>();
            services.AddSingleton<CandidateResolutionMetricsProvider>();
            services.AddSingleton<ICandidateResolutionSnapshotProvider, CandidateResolutionSnapshotProvider>();
            services.AddSingleton<CandidateResolverSelector>();
            services.AddSingleton<CandidateBenchmarkService>();


            //===========================================================================
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