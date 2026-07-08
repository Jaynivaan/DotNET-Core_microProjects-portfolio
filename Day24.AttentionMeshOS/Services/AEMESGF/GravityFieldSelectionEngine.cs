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
        private readonly ICandidateResolver _candidateResolver;
        private readonly GravityProximityCalculator _proximityCalculator;
        private readonly GravityOptions _options;
        private readonly ILogger<GravityFieldSelectionEngine> _logger;

        public GravityFieldSelectionEngine(
            IGravityRuntime runtime,
            ICandidateResolver candidateResolver,
            GravityProximityCalculator proximityCalculator,
            IOptions<GravityOptions> options,
            ILogger<GravityFieldSelectionEngine> logger
            )
        {
            _runtime = runtime;
            _candidateResolver = candidateResolver;
            _proximityCalculator = proximityCalculator;
            _options = options.Value;
            _logger = logger;
        }

        public GravityFieldSelectionResult SelectField(
            GravityFormationContext context)
        {
            _logger.LogInformation("Selection 1: start.");
            if ( !_options .Enabled ||
                context.TernarySignature.Length ==0 ||
                context.PresenceMask.Length == 0)
            {
                return new GravityFieldSelectionResult(
                    null,
                    0f,
                    false);
            }

            var candidateContext = new CandidateResolutionContext(
                context.TernarySignature,
                context.PresenceMask,
                DateTimeOffset.UtcNow,
                context.DynamicTagId);

            _logger.LogInformation("Selection 2: before _candidateResolver.Resolve(candidateContext);..");
            CandidateResolutionResult CandidateResult = _candidateResolver.Resolve( candidateContext );
            _logger.LogInformation("Selection 3: after _candidateResolver.Resolve(CandidateContext);..");

            IReadOnlyList<GravityFieldNode> fields = _runtime.Fields;

            GravityFieldNode? bestField = null;
            float bestScore = 0f;

            foreach (CandidateFieldRef candidate in CandidateResult.Candidates )
            {
                if ( candidate.RuntimeIndex < 0 ||
                    candidate.RuntimeIndex >= fields.Count)
                {
                    continue;
                }

                GravityFieldNode field = fields[candidate.RuntimeIndex];

                if (!field.IsAllocated ||
                    field.FieldId != candidate.FieldId)
                {
                    continue;
                }

                float proximity =
                    _proximityCalculator.Calculate(
                        context,
                        field,
                        _options);

                if(proximity >= _options.ResonanceThreshold &&
                    proximity > bestScore)
                {
                    bestScore = proximity;
                    bestField = field;
                }
            }           

            if ( bestField is null )
            {
                AemEsgfTelemetry.GravityFieldSelectionFailed(
                _logger,
                context.DynamicTagId);

                return new GravityFieldSelectionResult(
                    null,
                    bestScore,
                    false);
            }

            AemEsgfTelemetry.GravityFieldSelected(
                _logger,
                context.DynamicTagId,
                bestField.FieldId,
                Math.Round(bestScore, 4));
            _logger.LogInformation("Selection 4: completed..");
            return new GravityFieldSelectionResult(
                bestField,
                bestScore,
                true );

        }       
    }
}