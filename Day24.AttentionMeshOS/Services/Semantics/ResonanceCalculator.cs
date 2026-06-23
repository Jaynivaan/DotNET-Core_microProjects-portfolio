//gs
using System.Numerics;
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class ResonanceCalculator : IResonanceCalculator
    {
        private readonly ILogger<ResonanceCalculator> _logger;
        private readonly ResonanceOptions _options;

        public ResonanceCalculator(
            ILogger<ResonanceCalculator> logger,
            IOptions<ResonanceOptions> options )
        {
            _logger = logger;
            _options = options.Value;

        }

        public ResonanceResult Calculate(
            HyperVectorPayload source,
            HyperVectorPayload target)
        {
            if ( !_options .Enabled )
            {
                return new ResonanceResult(
                    source.AttentionBallId,
                    target.AttentionBallId,
                    0);

            }

            var sourceValues =  source.Values;
            var targetValues = target.Values;
            var length = sourceValues.Length;


            if ( length == 0  ||
                    targetValues.Length == 0 ||
                    length != targetValues.Length)
            {
                return new ResonanceResult(
                    source.AttentionBallId,
                    target.AttentionBallId,
                    0);

            }

            float dotProduct = 0f;
            float sourceMagnitude = 0f;
            float targetMagnitude = 0f;

            var vectorSize = Vector<float>.Count;
            var i = 0;

            for (; i <= length - vectorSize; i += vectorSize)
            {
                var sourceVector = new Vector<float>(
                    sourceValues,
                    i);

                var targetVector = new Vector<float>(
                    targetValues,
                    i);
                
                dotProduct += Vector.Dot(
                    sourceVector,
                    targetVector);

                sourceMagnitude += Vector.Dot(
                    sourceVector,
                    sourceVector);

                targetMagnitude += Vector.Dot(
                    targetVector,
                    targetVector);
            }

            for (; i < length;  i++)
            {
                var sourceValue = sourceValues[i]; 
                var targetValue = targetValues[i];

                dotProduct += sourceValue * targetValue;
                sourceMagnitude += sourceValue * sourceValue;
                targetMagnitude += targetValue * targetValue;

            }

            if ( sourceMagnitude <= 0f || targetMagnitude <= 0f )
            {
                return new ResonanceResult(
                    source.AttentionBallId,
                    target.AttentionBallId,
                    0);

            }

            var resonanceScore =
                (double)(
                    dotProduct /
                    MathF.Sqrt(
                        sourceMagnitude * targetMagnitude));

            resonanceScore = Math.Clamp(
                resonanceScore,
                -1.0,
                1.0);

            _logger.LogInformation(
                "Resonance calculated {SourceId} -> {TargetId}. Score = {Score}.",
                source.AttentionBallId,
                target.AttentionBallId,
                Math.Round(resonanceScore, 4));

            return new ResonanceResult(
                source.AttentionBallId,
                target.AttentionBallId,
                resonanceScore); 

        }
    }
}