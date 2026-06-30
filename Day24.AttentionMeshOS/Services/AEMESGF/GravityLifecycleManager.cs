//gs
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Options;
using Microsoft.Extensions.Options;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class GravityLifecycleManager : IGravityLifecycleManager
    {
        private readonly GravityOptions _options;
        private readonly ILogger<GravityLifecycleManager> _logger;

        public GravityLifecycleManager(
            IOptions<GravityOptions> options,
            ILogger<GravityLifecycleManager> logger
            )
        {
            _options = options.Value;
            _logger = logger;
        }

        public GravityLifecycleEvaluationResult Evaluate( GravityFieldNode field )
        {
            GravityFieldLifecycleState previousState = field.LifecycleState;

            GravityFieldLifecycleState nextState = ResolveState(field);


            if ( previousState != nextState )
            {
                field.LifecycleState = nextState;
                field.LastEvolvedAt = DateTimeOffset.UtcNow;

                AemEsgfTelemetry.GravityLifecycleChanged(
                    _logger,
                    field.FieldId,
                    previousState.ToString(),
                    nextState.ToString(),
                    field.SemanticMass,
                    field.AttentionEnergy,
                    field.StabilityScore);                
            }
            return new GravityLifecycleEvaluationResult(
                    StateChanged: previousState != nextState,
                    PreviousState: previousState,
                    CurrentState: nextState);
        }
        private GravityFieldLifecycleState ResolveState(GravityFieldNode field)
        {
            if ( !field.IsAllocated )
            {
                return GravityFieldLifecycleState.Dormant;
            }

            if (  field.SemanticMass >= _options.MaxSemanticMass &&
                field.StabilityScore >= _options.StabilityThreshold)
            {
                return GravityFieldLifecycleState.Dominant;
            }

            if ( field.StabilityScore >= _options.StabilityThreshold )
            {
                return GravityFieldLifecycleState.Stable;
            }

            if ( field.SemanticMass > 0f ||
                field.Participations.Count > 0 )
            {
                return GravityFieldLifecycleState.Emerging;
            }
            return GravityFieldLifecycleState.Dormant;
        }
    }
}