//gs
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Options;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class GravityFieldSelectionEngine
    {
        private readonly IGravityRuntime _runtime;
        private readonly GravityProximityCalculator _proximityCalculator;
        private readonly GravityOptions _options;
        private readonly ILogger<GravityFieldSelectionEngine> _logger;

        public GravityFieldSelectionEngine(
            IGravityRuntime runtime,
            GravityProximityCalculator proximityCalculator,
            IOptions<GravityOptions> options,
            ILogger<GravityFieldSelectionEngine> logger
            )
        {
            _runtime = runtime;
            _proximityCalculator = proximityCalculator;
            _options = options.Value;
            _logger = logger;
        }

        public GravityFieldSelectionResult SelectField(
            GravityFormationContext context)
        {
            if ( !_options .Enabled ||
                context.TernarySignature.Length ==0 ||
                context.PresenceMask.Length == 0)
            {
                return new GravityFieldSelectionResult(
                    null,
                    0f,
                    false);
            }

            ReadOnlySpan<GravityFieldNode> fields = _runtime.Fields;

            GravityFieldNode? bestField = null;
            float bestScore = 0f;

            for ( int i = 0; i < fields.Length; i++ )
            {
                GravityFieldNode field = fields[i];

                if ( !field .IsAllocated )
                {
                    continue;
                }

                float proximity =
                    _proximityCalculator.Calculate(
                    context,
                    field,
                    _options);
                    

                if ( proximity >= _options.ResonanceThreshold  &&
                    proximity > bestScore)
                {
                    bestScore = proximity;
                    bestField = field;
                }
            }

            if ( bestField is null )
            {
                _logger.LogInformation(
                    "No Matching Gravity Field found for DynamicTagId={DynamicTagId}.",
                    context.DynamicTagId);

                return new GravityFieldSelectionResult(
                    null,
                    bestScore,
                    false);
            }

            _logger.LogInformation(
                "Gravity Field Selected. DynamicTagId={DynamicTagId}, FieldId={FieldId}, Proximity={Proximity}.",
                context.DynamicTagId,
                bestField.FieldId,
                Math.Round(bestScore, 4));
            
            return new GravityFieldSelectionResult(
                bestField,
                bestScore,
                true );

        }       
    }
}