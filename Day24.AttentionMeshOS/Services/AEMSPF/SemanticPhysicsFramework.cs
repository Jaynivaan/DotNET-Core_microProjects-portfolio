//gs
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;
using Microsoft.Extensions.Logging;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class SemanticPhysicsFramework : ISemanticPhysicsFramework
    {
        private readonly IReadOnlyList<ISemanticPhysicsLaw> _laws;
        private readonly ILogger<SemanticPhysicsFramework> _logger;

        public SemanticPhysicsFramework(
            IEnumerable<ISemanticPhysicsLaw> laws,
            ILogger<SemanticPhysicsFramework> logger)
        {
            _laws = laws.ToList();
            _logger = logger;
        }
        
        public SemanticPhysicsResult Evaluate(SemanticPhysicsContext context)
        {
            AemEsgfTelemetry.PhysicsEvaluationStarted(
                _logger,
                context.Field.FieldId,
                context.SemanticMass,
                context.ResonanceScore);

            SemanticPhysicsResult result = 
                SemanticPhysicsResult.FromState(context.CurrentState);

            for (int i = 0; i < _laws.Count; i++)
            {
                ISemanticPhysicsLaw law = _laws[i];
                result = law.Evaluate(context, result);
            }

            AemEsgfTelemetry.AttentionEnergyUpdated(
                _logger,
                context.Field.FieldId,
                context.CurrentState.AttentionEnergy,
                result.AttentionEnergy);

            AemEsgfTelemetry.StabilityUpdated(
                _logger,
                context.Field.FieldId,
                context.CurrentState.Stability,
                result.Stability);

            AemEsgfTelemetry.RadiusUpdated(
                _logger,
                context.Field.FieldId,
                context.CurrentState.Radius,
                result.Radius);

            AemEsgfTelemetry.AttractionPotentialCalculated(
                _logger,
                context.Field.FieldId,
                result.AttractionPotential);

            AemEsgfTelemetry.SemanticMomentumCalculated(
                _logger,
                context.Field.FieldId,                
                result.SemanticMomentum);

            AemEsgfTelemetry.PhysicsEvaluationCompleted(
                _logger,
                context.Field.FieldId,
                result.AttentionEnergy,
                result.Stability,
                result.Radius,
                result.AttractionPotential,
                result.SemanticMomentum
                );
            return result;
        }
    }
}