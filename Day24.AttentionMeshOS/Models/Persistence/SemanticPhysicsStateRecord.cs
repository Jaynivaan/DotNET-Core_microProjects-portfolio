//gs
using System;

namespace Day24.AttentionMeshOS.Models
{
    public sealed record SemanticPhysicsStateRecord(

        float AttentionEnergy,
        float Stability,
        float Radius,
        float AttractionPotential,
        float SemanticMomentum,
        float PreviousAttentionEnergy,
        float PreviousStability,
        float PreviousRadius,
        DateTimeOffset PreviousUpdatedAt,
        DateTimeOffset LastUpdatedAt

        );

}