//gs
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;
using System;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class StabilityLaw : ISemanticPhysicsLaw
    {
        public SemanticPhysicsResult Evaluate(SemanticPhysicsContext context, SemanticPhysicsResult current)
        {
            float maxEnergy = Math.Max(
                0.001f,
                context.Options.MaximumEnergy);
            
            float energyFactor = Math.Clamp(
                current.AttentionEnergy / maxEnergy,
                0f,
                1f);

            float participationFactor = 
                context.ParticipationMetrics.TotalParticipations <= 0
                ? 0f
                : (float)Math.Min(
                    context.ParticipationMetrics.AverageReinforcementCount / 10d,
                    1d);

            float lifecycleFactor = GetLifecycleFactor(
                context.LifecycleState);

            float totalFactor = (context.ResonanceScore + energyFactor + participationFactor + lifecycleFactor) / 4f;

            float stabilityIncrease = context.Options.StabilityReinforcementRate * totalFactor;

            float nextStability = Math.Clamp(
                current.Stability + stabilityIncrease,
                context.Options.MinimumStability,
                context.Options.MaximumStability);

            return current with
            {
                Stability = nextStability
            };
        }

        private static float GetLifecycleFactor(
            GravityFieldLifecycleState state)
        {
            return state switch
            {
                GravityFieldLifecycleState.Dormant => 0.0f,
                GravityFieldLifecycleState.Emerging => 0.025f,
                GravityFieldLifecycleState.Stable => 0.75f,
                GravityFieldLifecycleState.Dominant => 1.0f,
                GravityFieldLifecycleState.Dissipating => 0f,
                _ => 0f
            };
        }
    }
}