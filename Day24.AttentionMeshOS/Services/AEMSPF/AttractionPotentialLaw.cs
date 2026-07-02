//gs
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class AttractionPotentialLaw : ISemanticPhysicsLaw
    {
        public SemanticPhysicsResult Evaluate(
            SemanticPhysicsContext context,
            SemanticPhysicsResult current)
        {
            float normalizedMass = 
                context.SemanticMass <= 0f
                    ? 0f
                    : context.SemanticMass / (context.SemanticMass + 1f);

            float maxEnergy = Math.Max(
                0.001f,
                context.Options.MaximumEnergy);

            float normalizedEnergy = Math.Clamp(
                current.AttentionEnergy / maxEnergy,
                0f,
                1f);

            float maxRadius = Math.Max(
                0.001f,
                context.Options.MaximumRadius);

            float normalizedRadius = Math.Clamp(
                current.Radius / maxRadius,
                0f,
                1f);

            float potential =
                (context.Options.PotentialMassWeight * normalizedMass) +
                (context.Options.PotentialEnergyWeight * normalizedEnergy) +
                (context.Options.PotentialStabilityWeight * current.Stability) +
                (context.Options.PotentialRadiusPenalty * normalizedRadius);

            potential = Math.Clamp(
                potential,
                0f,
                1f);

            return current with
            {
                AttractionPotential = potential
            };
        }
    }
}