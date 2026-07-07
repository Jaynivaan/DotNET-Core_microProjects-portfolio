//gs

using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class GravityMergeEvaluator : IGravityMergeEvaluator
    {
        public GravityMergeCandidate Evaluate(
            GravityFieldNode source,
            GravityFieldNode target)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(target);

            double similarity =
                CalculateSignatureSimilarity(
                    source.FieldSignature,
                    target.FieldSignature);

            double massRatio =
                CalculateMassRatio(
                    source.SemanticMass,
                    target.SemanticMass);

            double StabilityScore =
                Math.Min(
                    source.StabilityScore,
                    target.StabilityScore);

            return new GravityMergeCandidate(
                source.FieldId,
                target.FieldId,
                similarity,
                massRatio,
                StabilityScore,
                "Merge candidate evaluated.");
                


        }

        private static double CalculateSignatureSimilarity(
            sbyte[] source,
            sbyte[] target)
        {
            int length = Math.Min(source.Length, target.Length);
            
            if ( length == 0 )
            {
                return 0d;
            }
            int compared = 0;
            int matched = 0;

            for ( int i = 0; i < length; i++ )
            {
                if (source[i] == 0 && target[i] == 0 )
                {
                    continue;
                }

                compared++;

                if (source[i] == target[i])
                {
                    matched++;
                }
            }
            
            if ( compared == 0 )
            {
                return 0d;
            }

            return (double)matched / compared;

        }

        private static double CalculateMassRatio(
            float sourceMass,
            float targetMass)
        {
            float smaller = Math.Min(sourceMass, targetMass);
            float larger = Math.Max(sourceMass, targetMass);

            if ( larger <= 0f )
            {
                return 0d;
            }

            return smaller / larger;
        }
    }
}