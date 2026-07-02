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


    }

}