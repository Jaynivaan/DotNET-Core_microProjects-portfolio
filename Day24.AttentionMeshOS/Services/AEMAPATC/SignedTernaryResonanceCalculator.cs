//gs
using System;
using Microsoft.Extensions.Logging;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class SignedTernaryResonanceCalculator
    {
        private readonly ILogger<SignedTernaryResonanceCalculator> _logger;

        public SignedTernaryResonanceCalculator(
            ILogger<SignedTernaryResonanceCalculator> logger)
        {
            _logger = logger;
        }

        public float Calculate(
            sbyte[]? incomingMask,
            sbyte[]? centroidMask)
        {
            if (incomingMask == null || centroidMask is null )
            {
                return 0.0f;
            }

            int length = Math.Min(
                incomingMask.Length,
                centroidMask.Length);

            if ( length == 0 )
            {
                return 0.0f;
            }

            float score = 0.0f;
            float maxScore = length;

            for ( int i = 0; i < length; i++ )
            {
                sbyte incoming  = incomingMask[i];
                sbyte centroid = centroidMask[i];

                if ( incoming  == centroid && incoming != 0 )
                {
                    score += 1.0f;
                }
                else if ( incoming == 0 && centroid == 0 )
                {
                    score += 0f;
                }
                else if ( incoming == 0 || centroid == 0 )
                {
                    score += 0.1f;
                }
                else
                {
                    score -= 0.5f;
                }
            }

            float finalScore = Math.Clamp(
                score / maxScore, 0.0f, 1.0f);

            _logger.LogInformation(
                "Signed ternary resonance = {Score:F3}",
                finalScore
                );
            return finalScore;

        }
    }
}