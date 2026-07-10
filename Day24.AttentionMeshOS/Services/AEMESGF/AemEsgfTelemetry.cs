//gs

using Microsoft.Extensions.Logging;

namespace Day24.AttentionMeshOS.Services
{
    public static partial class AemEsgfTelemetry
    {
        [LoggerMessage(
            EventId = 3001,
            Level = LogLevel.Information,
            Message = "Semantic Mass Updated. FieldId={FieldId}, PreviousMass={PreviousMass}, Increase={Increase}, CurrentMass={CurrentMass}.")]
        public static partial void SemanticMassUpdated(
            ILogger logger,
            Guid fieldId,
            float previousMass,
            float increase,
            float currentMass);

        [LoggerMessage(
            EventId = 3002,
            Level = LogLevel.Information,
            Message = "Gravity Lifecycle changed. FieldId={FieldId}, PreviousState={PreviousState}, CurrentState={CurrentState}, SemanticMass={SemanticMass}, AttentionEnergy={AttentionEnergy}, StabilityScore={StabilityScore}..")]
        public static partial void GravityLifecycleChanged(
            ILogger logger,
            Guid fieldId,
            string previousState,
            string currentState,
            float semanticMass,
            float attentionEnergy,
            float stabilityScore );

        [LoggerMessage(
            EventId = 3003,
            Level = LogLevel.Information,
            Message = "Gravity Runtime Initialized.  FieldCount={FieldCount}.")]
        public static partial void GravityRuntimeInitialized(
            ILogger logger,
            int fieldCount);

        [LoggerMessage(
            EventId = 3004,
            Level = LogLevel.Information,
            Message = "Gravity Field allocated. FieldId={FieldId}. ")]
        public static partial void GravityFieldAllocated(
            ILogger logger,
            Guid fieldId);

        [LoggerMessage(
            EventId = 3005,
            Level = LogLevel.Information,
            Message = "Gravity Field reset. FieldId={FieldId}.")]
        public static partial void GravityFieldReset(
            ILogger logger,
            Guid fieldId);

        [LoggerMessage(
            EventId = 3006,
            Level = LogLevel.Information,
            Message = "No Matching Gravity Field found. DynamicTagId={DynamicTagId},  ")]
        public static partial void GravityFieldSelectionFailed(
            ILogger logger,
            Guid dynamicTagId);

        [LoggerMessage(
            EventId = 3007,
            Level = LogLevel.Information,
            Message = "Gravity Field Selected.  DynamicTagId={DynamicTagId}, FieldId={FieldId}, Proximity={Proximity}. ")]
        public static partial void GravityFieldSelected(
            ILogger logger,
            Guid dynamicTagId,
            Guid fieldId,
            double proximity);

        [LoggerMessage(
            EventId = 3008,
            Level = LogLevel.Information,
            Message = "Gravity Field Created. FieldId={FieldId} ")]
        public static partial void GravityFieldCreated(
            ILogger logger,
            Guid fieldId);

        [LoggerMessage(
            EventId = 3009,
            Level = LogLevel.Information,
            Message = "DynamicTag matched with an Existing Gravity Field. FieldId={FieldId}. ")]
        public static partial void GravityFieldMatched(
            ILogger logger,
            Guid fieldId);

        [LoggerMessage(
            EventId = 3010,
            Level = LogLevel.Information,
            Message = "Dynamic Tag participation reinforced.  FieldId={FieldId}, DynamicTagId={DynamicTagId}, ReinforcementCount={ReinforcementCount}.  ")]
        public static partial void ParticipationReinforced(
            ILogger logger,
            Guid fieldId,
            Guid dynamicTagId,
            int reinforcementCount);

        [LoggerMessage(
            EventId = 3011,
            Level = LogLevel.Information,
            Message = "Gravity Field Signature updated. FieldId={FieldId}."  )]
        public static partial void GravityFieldSignatureUpdated(
            ILogger logger,
            Guid fieldId);

        [LoggerMessage(
            EventId = 3012,
            Level = LogLevel.Information,
            Message = "Gravity Runtime slab is full. Field allocation failed.")]
        public static partial void GravityRuntimeFull(
            ILogger logger);



        // AEMSPF Physics Telemetry
        [LoggerMessage(
            EventId = 3013,
            Level = LogLevel.Information,
            Message = "Physics evaluation started. FieldId={FieldId}, SemanticMass={SemanticMass}, ResonanceScore={ResonanceScore}.")]
        public static partial void PhysicsEvaluationStarted(
        ILogger logger,
        Guid fieldId,
        float semanticMass,
        float resonanceScore);

        [LoggerMessage(
            EventId = 3014,
            Level = LogLevel.Information,
            Message = "Physics evaluation completed. FieldId={FieldId}, Energy={Energy}, Stability={Stability}, Radius={Radius}, Potential={Potential}, Momentum={Momentum}.")]
        public static partial void PhysicsEvaluationCompleted(
        ILogger logger,
        Guid fieldId,
        float energy,
        float stability,
        float radius,
        float potential,
        float momentum);

        [LoggerMessage(
            EventId = 3015,
            Level = LogLevel.Information,
            Message = "Attention Energy updated. FieldId={FieldId}, PreviousEnergy={PreviousEnergy}, CurrentEnergy={CurrentEnergy}.")]
        public static partial void AttentionEnergyUpdated(
        ILogger logger,
        Guid fieldId,
        float previousEnergy,
        float currentEnergy);

        [LoggerMessage(
            EventId = 3016,
            Level = LogLevel.Information,
            Message = "Stability updated. FieldId={FieldId}, PreviousStability={PreviousStability}, CurrentStability={CurrentStability}.")]
        public static partial void StabilityUpdated(
        ILogger logger,
        Guid fieldId,
        float previousStability,
        float currentStability);

        [LoggerMessage(
            EventId = 3017,
            Level = LogLevel.Information,
            Message = "Radius updated. FieldId={FieldId}, PreviousRadius={PreviousRadius}, CurrentRadius={CurrentRadius}.")]
        public static partial void RadiusUpdated(
        ILogger logger,
        Guid fieldId,
        float previousRadius,
        float currentRadius);

        [LoggerMessage(
            EventId = 3018,
            Level = LogLevel.Information,
            Message = "Attraction Potential calculated. FieldId={FieldId}, Potential={Potential}.")]
        public static partial void AttractionPotentialCalculated(
        ILogger logger,
        Guid fieldId,
        float potential);

        [LoggerMessage(
            EventId = 3019,
            Level = LogLevel.Information,
            Message = "Semantic Momentum calculated. FieldId={FieldId}, Momentum={Momentum}.")]
        public static partial void SemanticMomentumCalculated(
        ILogger logger,
        Guid fieldId,
        float momentum);

        [LoggerMessage(
            EventId = 3020,
            Level = LogLevel.Information,
            Message = "Previous physics state captured. FieldId={FieldId}.")]
        public static partial void PreviousPhysicsStateCaptured(
        ILogger logger,
        Guid fieldId);

        [LoggerMessage(
            EventId = 3021,
            Level = LogLevel.Information,
            Message = "Physics state committed. FieldId={FieldId}, Energy={Energy}, Stability={Stability}, Radius={Radius}, Potential={Potential}, Momentum={Momentum}.")]
        public static partial void PhysicsStateCommitted(
        ILogger logger,
        Guid fieldId,
        float energy,
        float stability,
        float radius,
        float potential,
        float momentum);

        //persistance logs

        [LoggerMessage(
            EventId = 3022,
            Level = LogLevel.Information,
            Message = "Persistence save started.FormatVersion={FormatVersion}.")]
        public static partial void PersistenceSaveStarted(
        ILogger logger,
        int formatVersion);

        [LoggerMessage(
            EventId = 3023,
            Level = LogLevel.Information,
            Message = "Persistence save completed. FormatVersion={FormatVersion}.")]
        public static partial void PersistenceSaveCompleted(
        ILogger logger,
        int formatVersion);

        [LoggerMessage(
            EventId = 3024,
            Level = LogLevel.Error,
            Message = "Persistence save failed.")]
        public static partial void PersistenceSaveFailed(
        ILogger logger,
        Exception exception);

        [LoggerMessage(
            EventId = 3025,
            Level = LogLevel.Information,
            Message = "Persistence load started.")]
        public static partial void PersistenceLoadStarted(
        ILogger logger);

        [LoggerMessage(
            EventId = 3026,
            Level = LogLevel.Information,
            Message = "Persistence load completed. SaveFound={SaveFound}.")]
        public static partial void PersistenceLoadCompleted(
        ILogger logger,
        bool saveFound);

        [LoggerMessage(
            EventId = 3027,
            Level = LogLevel.Error,
            Message = "Persistence load failed.")]
        public static partial void PersistenceLoadFailed(
        ILogger logger,
        Exception exception);

        [LoggerMessage(
            EventId = 3028,
            Level = LogLevel.Information,
            Message = "Persistence Validation Started. FormatVersion={FormatVersion}.")]
        public static partial void PersistenceValidationStarted(
        ILogger logger,
        int formatVersion);

        [LoggerMessage(
            EventId = 3029,
            Level = LogLevel.Error,
            Message = "Persistence validation failed.")]
        public static partial void PersistenceValidationFailed(
        ILogger logger,
        Exception exception);

        [LoggerMessage(
            EventId = 3030,
            Level = LogLevel.Information,
            Message = "Persistence restore started.")]
        public static partial void PersistenceRestoreStarted(
        ILogger logger );

        [LoggerMessage(
            EventId = 3031,
            Level = LogLevel.Information,
            Message = "Persistence Restore completed.")]
        public static partial void PersistenceRestoreCompleted(
        ILogger logger );

        [LoggerMessage(
            EventId = 3032,
            Level = LogLevel.Error,
            Message = "Persistence restore failed.")]
        public static partial void PersistenceRestoreFailed(
        ILogger logger,
        Exception exception);


        //candidate resolution telemetry

        [LoggerMessage(
            EventId = 3033,
            Level = LogLevel.Information,
            Message = "Candidate Resolution started. Resolver={ResolverName}.")]
        public static partial void CandidateResolutionStarted(
        ILogger logger,
        string resolverName);

        [LoggerMessage(
            EventId = 3034,
            Level = LogLevel.Information,
            Message = "Candidate Resolution completed.  Resolver={ResolverName}, CandidateCount={CandidateCount}, UsedFallback={UsedFallback}.")]
        public static partial void CandidateResolutionCompleted(
        ILogger logger,
        string resolverName,
        int candidateCount,
        bool usedFallback);

        [LoggerMessage(
            EventId = 3035,
            Level = LogLevel.Warning,
            Message = "Candidate Resolution fallback used. Resolver={ResolverName}.")]
        public static partial void CandidateFallbackUsed(
        ILogger logger,
        string resolverName);

        [LoggerMessage(
            EventId = 3036,
            Level = LogLevel.Information,
            Message = "Candidate Resolver selected. Resolver={ResolverName}.")]
        public static partial void CandidateResolverSelected(
        ILogger logger,
        string resolverName);

        [LoggerMessage(
            EventId = 3037,
            Level = LogLevel.Information,
            Message = "Candidate Count bounded. Resolver={ResolverName}, OriginalCount={OriginalCount}, BoundedCount={BoundedCount}.")]
        public static partial void CandidateCountBounded(
        ILogger logger,
        string resolverName,
        int originalCount,
        int boundedCount);


        //semantic Quantization Telemetry

        [LoggerMessage(
            EventId = 3038,
            Level = LogLevel.Information,
            Message = "Semantic Bucket registered. BucketCode={BucketCode}, FieldId={FieldId}, RuntimeIndex={RuntimeIndex}.")]
        public static partial void BucketRegistered(
        ILogger logger,
        int bucketCode,
        Guid fieldId,
        int runtimeIndex);

        [LoggerMessage(
            EventId = 3039,
            Level = LogLevel.Information,
            Message = "Semantic Bucket unregistered. BucketCode={BucketCode},  FieldId={FieldId},  RuntimeIndex={RuntimeIndex}.")]
        public static partial void BucketUnregistered(
        ILogger logger,
        int bucketCode,
        Guid fieldId,
        int runtimeIndex);


        [LoggerMessage(
            EventId = 3040,
            Level = LogLevel.Information,
            Message = "Semantic Bucket lookup completed. BucketCode={BucketCode}, CandidateCount={CandidateCount}.")]
        public static partial void BucketLookupCompleted(
        ILogger logger,
        int bucketCode,
        int candidateCount);

        [LoggerMessage(
            EventId = 3041,
            Level = LogLevel.Information,
            Message = "Semantic Bucket neighbor expansion completed. CenterBucketCode={CenterBucketCode}, NeighborCount={NeighborCount}.")]
        public static partial void BucketNeighborExpansionCompleted(

        ILogger logger,
        int centerBucketCode,
        int neighborCount);


        [LoggerMessage(
            EventId = 3042,
            Level = LogLevel.Information,
            Message = "Semantic Bucket metrics Captured. BucketCount={BucketCount}, TotalEntries={TotalEntries}, AverageOccupancy={AverageOccupancy}, LargestBucketSize={LargestBucketSize}, SmallestBucketSize={SmallestBucketSize}.")]
        public static partial void BucketMetricsCaptured(
        ILogger logger,
        int bucketCount,
        int totalEntries,
        double AverageOccupancy,
        int largestBucketSize,
        int smallestBucketSize);

        //gravity evolution telemetry


        [LoggerMessage(
            EventId = 3043,
            Level = LogLevel.Information,
            Message = "Gravity evolution cycle started. FieldCount={FieldCount}, EvaluationTime={EvaluationTime}.")]
        public static partial void GravityEvolutionStarted(
        ILogger logger,
        int fieldCount,
        DateTimeOffset evaluationTime);

        [LoggerMessage(
           EventId = 3044,
           Level = LogLevel.Information,
           Message = "Gravity merge candidate Evaluated. SourceFieldId={SourceFieldId}, TargetFieldId={TargetFieldId}, SimilarityScore={SimilarityScore}, MassRatio={MassRatio}, StabilityScore={StabilityScore}.")]
        public static partial void GravityMergeCandidateEvaluated(
        ILogger logger,
        Guid sourceFieldId,
        Guid targetFieldId,
        double similarityScore,
        double massRatio,
        double stabilityScore);

        [LoggerMessage(
           EventId = 3045,
           Level = LogLevel.Information,
           Message = "Gravity merge decision completed. SourceFieldId={SourceFieldId}, TargetFieldId={TargetFieldId}, Approved={Approved}, Reason={Reason}.")]
        public static partial void GravityMergeDecisionCompleted(
        ILogger logger,
        Guid sourceFieldId,
        Guid targetFieldId,
        bool approved,
        string reason);

        [LoggerMessage(
           EventId = 3046,
           Level = LogLevel.Information,
           Message = "Gravity merge Executed. SourceFieldId={SourceFieldId}, TargetFieldId={TargetFieldId}, ExecutedAt={ExecutedAt}.")]
        public static partial void GravityMergeExecuted(
        ILogger logger,
        Guid sourceFieldId,
        Guid targetFieldId,
        DateTimeOffset executedAt);

        [LoggerMessage(
           EventId = 3047,
           Level = LogLevel.Information,
           Message = "Gravity dissolution candidate Evaluated. FieldId={FieldId}, AttentionEnergy={AttentionEnergy}, Stability={Stability}, SemanticMass={SemanticMass}, ParticipantCount={ParticipantCount}.")]
        public static partial void GravityDissolutionCandidateEvaluated(
        ILogger logger,
        Guid fieldId,       
        float attentionEnergy,
        float stability,
        float semanticMass,
        int ParticipantCount);

        [LoggerMessage(
           EventId = 3048,
           Level = LogLevel.Information,
           Message = "Gravity dissolution decision completed.FieldId={FieldId}, Approved={Approved}, Reason={Reason}.")]
        public static partial void GravityDissolutionDecisionCompleted(
        ILogger logger,
        Guid fieldId,
        bool approved,
        string reason);

        [LoggerMessage(
           EventId = 3049,
           Level = LogLevel.Information,
           Message = "Gravity field Dissolved. FieldId={FieldId}, DissolvedAt={DissolvedAt}.")]
        public static partial void GravityFieldDissolved(
        ILogger logger,
        Guid fieldId,
        DateTimeOffset dissolvedAt);

        [LoggerMessage(
           EventId = 3050,
           Level = LogLevel.Information,
           Message = "Gravity evolution cycle completed. MergeCandidates={MergeCandidates}, MergesExecuted={MergesExecuted}, DissolutionCandidates={DissolutionCandidates}, DissolutionsExecuted={DissolutionsExecuted},EvolutionPerformed={EvolutionPerformed}.")]
        public static partial void GravityEvolutionCompleted(
        ILogger logger,
        int mergeCandidates,
        int mergesExecuted,
        int dissolutionCandidates,
        int dissolutionsExecuted,
        bool evolutionPerformed);




    }

}