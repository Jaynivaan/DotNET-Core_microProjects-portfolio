//gs
using System;
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class SemanticPhysicsPersistenceSerializer : ISemanticPhysicsPersistenceSerializer
    {
        public SemanticPhysicsStateRecord Capture(
            SemanticPhysicsState state )
        {
            ArgumentNullException.ThrowIfNull(state);

            return new SemanticPhysicsStateRecord(
                state.AttentionEnergy,
                state.Stability,
                state.Radius,
                state.AttractionPotential,
                state.SemanticMomentum,
                state.PreviousAttentionEnergy,
                state.PreviousStability,
                state.PreviousRadius,
                state.PreviousUpdatedAt,
                state.LastUpdatedAt);
        }

        public void Restore(
            SemanticPhysicsState state,
            SemanticPhysicsStateRecord record)
        {
            ArgumentNullException.ThrowIfNull(state);
            ArgumentNullException.ThrowIfNull(record);

            state.AttentionEnergy = record.AttentionEnergy;
            state.Stability = record.Stability;
            state.Radius = record.Radius;
            state.AttractionPotential = record.AttractionPotential;
            state.SemanticMomentum = record.SemanticMomentum;
            state.PreviousAttentionEnergy = record.PreviousAttentionEnergy;
            state.PreviousStability = record.PreviousStability;
            state.PreviousRadius = record.PreviousRadius;
            state.PreviousUpdatedAt = record.PreviousUpdatedAt;
            state.LastUpdatedAt = record.LastUpdatedAt;
        }
    }
}