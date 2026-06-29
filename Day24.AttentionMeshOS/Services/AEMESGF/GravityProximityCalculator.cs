//gs
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Options;
namespace Day24.AttentionMeshOS.Services
{
    public sealed class GravityProximityCalculator
    {
        public float Calculate(
            GravityFormationContext context,
            GravityFieldNode field,
            GravityOptions options)
        {
            if (!field .IsAllocated ||
                    context.TernarySignature.Length == 0 ||
                    context.PresenceMask.Length == 0 )
            {
                return 0f;
            }

            float ternaryResonance = CalculateMaskedTernaryResonance(
                context.TernarySignature,
                context.PresenceMask,
                field.FieldSignature);

            float vocabularySimilarity = 0f;

            float proximity =
                (ternaryResonance * options.SignedTernaryWeight) +
                (vocabularySimilarity * options.VocabularyWeight);

            return Math.Clamp(proximity, 0f, 1f);
        }

        private static float CalculateMaskedTernaryResonance(
            ReadOnlySpan<sbyte> incomingMask,
            ReadOnlySpan<sbyte> presenceMask,
            ReadOnlySpan<sbyte> fieldMask)
        {
            int length = Math.Min(incomingMask.Length, fieldMask.Length);
            length = Math.Min(length, presenceMask.Length);

            if (length == 0)
            {
                return 0f;
            }

            int matchingDimensions = 0;
            int activeDimensions = 0;

            for (int i = 0; i < length; i++)
            {
                if ( presenceMask[i] == 0 )
                {
                    continue;
                }

                sbyte incomingValue = incomingMask[i];
                sbyte fieldValue = fieldMask[i];

                if ( incomingValue != 0 || fieldValue != 0)
                {
                    activeDimensions++;
                    if ( incomingValue == fieldValue )
                    {
                        matchingDimensions++;
                    }
                }
            }

            if ( activeDimensions == 0 )
            {
                return 0f;
            }

            return (float)matchingDimensions / activeDimensions;
        }
    }
}