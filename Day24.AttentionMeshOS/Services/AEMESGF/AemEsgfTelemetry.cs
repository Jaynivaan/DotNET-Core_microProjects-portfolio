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

    }

}