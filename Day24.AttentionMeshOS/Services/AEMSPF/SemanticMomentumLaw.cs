//gs
using System;
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class SemanticMomentumLaw : ISemanticPhysicsLaw
    {
        public SemanticPhysicsResult Evaluate(
            SemanticPhysicsContext context,
            SemanticPhysicsResult current)
        {
            SemanticPhysicsState previous = context.CurrentState;

            float normalizedMass =
                context.SemanticMass <= 0f
                    ? 0f
                    : context.SemanticMass / (context.SemanticMass + 1f);

            float maxEnergy = Math.Max(
                0.001f,
                context.Options.MaximumEnergy);

            float previousEnergy = Math.Clamp(
                previous.PreviousAttentionEnergy / maxEnergy,
                0f,
                1f);

            float currentEnergy = Math.Clamp(
                current.AttentionEnergy / maxEnergy,
                0f,
                1f);

            float maxRadius = Math.Max(
                0.001f,
                context.Options.MaximumRadius);

            float previousRadius = Math.Clamp(
                previous.PreviousRadius / maxRadius,
                0f,
                1f);

            float currentRadius = Math.Clamp(
                current.Radius / maxRadius,
                0f,
                1f);

            float previousPotential =
                CalculatePotential(
                    context,
                    normalizedMass,
                    previousEnergy,
                    previous.PreviousStability,
                    previousRadius);

            float deltaEnergy = 
                currentEnergy - previousEnergy;

            float deltaStability =
                current.Stability - previous.PreviousStability;

            float deltaRadius =
                currentRadius - previousRadius;

            float deltaPotential =
                current.AttractionPotential - previousPotential;

            float averageDelta =
                ( deltaEnergy +
                  deltaStability +
                  deltaRadius +
                  deltaPotential) / 4f;

            float finalMomentum =
                Math.Clamp(
                    averageDelta * context.Options.MomentumSensitivity,
                    -1f,
                    1f);

            return current with
            {
                SemanticMomentum = finalMomentum
            };
        }

        private static float CalculatePotential(
            SemanticPhysicsContext context,
            float normalizedMass,
            float normalizedEnergy,
            float stability,
            float normalizedRadius)
        {
            float potential = 
                (context.Options.PotentialMassWeight * normalizedMass) +
                (context.Options.PotentialEnergyWeight * normalizedEnergy) +
                (context.Options.PotentialStabilityWeight * stability) -
                (context.Options.PotentialRadiusPenalty * normalizedRadius);

            return Math.Clamp(
                potential,
                0f,
                1f);
        }
    }
}