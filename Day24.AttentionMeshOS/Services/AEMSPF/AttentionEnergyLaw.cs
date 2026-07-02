//gs
using System;
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class AttentionEnergyLaw : ISemanticPhysicsLaw
    {
        public SemanticPhysicsResult Evaluate(
            SemanticPhysicsContext context,
            SemanticPhysicsResult current)
        {
            float massFactor = context.SemanticMass <= 0f
                ? 0f
                : context.SemanticMass / (context.SemanticMass + 1f);

            float reinforcementFactor = 
                context.ParticipationMetrics.TotalParticipations <= 0
                ? 0f
                : (float)Math.Min(
                    context.ParticipationMetrics.AverageReinforcementCount / 10d,
                    1d);

            float increase =
                context.Options.EnergyReinforcementRate *
                (context.ResonanceScore + massFactor + reinforcementFactor);

            float nextEnergy = Math.Clamp(
                current.AttentionEnergy + increase,
                context.Options.MinimumEnergy,
                context.Options.MaximumEnergy);

            return current with
            {
                AttentionEnergy = nextEnergy
            };
        }
    }
}