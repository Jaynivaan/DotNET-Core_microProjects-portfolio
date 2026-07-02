//gs
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class RadiusLaw : ISemanticPhysicsLaw
    {
        public SemanticPhysicsResult Evaluate(
            SemanticPhysicsContext context,
            SemanticPhysicsResult current)
        {
            float participationFactor =
                context.ParticipationMetrics.TotalParticipations <= 0
                    ? 0f
                    : Math.Clamp(
                        context.ParticipationMetrics.TotalParticipations / 10f,
                        0f,
                        1f);

            float maxEnergy = Math.Max(
                0.001f,
                context.Options.MaximumEnergy);

            float energyFactor = Math.Clamp(
                current.AttentionEnergy / maxEnergy,
                0f,
                1f);

            float spreadFactor =
                (participationFactor + energyFactor) / 2f;

            float coherenceFactor =
                (current.Stability + context.ResonanceScore) / 2f;

            float netDisplacement =
                context.Options.RadiusExpansionRate * spreadFactor;

            if (coherenceFactor > 0.75f)
            {
                netDisplacement -=
                    context.Options.RadiusContractionRate * (coherenceFactor - 0.75f);
            }

            float nextRadius = Math.Clamp(
                current.Radius + netDisplacement,
                context.Options.MinimumRadius,
                context.Options.MaximumRadius);
            
                return current with
                {
                    Radius = nextRadius
                };
        }
    }
}