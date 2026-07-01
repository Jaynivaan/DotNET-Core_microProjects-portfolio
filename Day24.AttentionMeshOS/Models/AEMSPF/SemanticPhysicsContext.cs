//gs
using System;

using Day24.AttentionMeshOS.Options;

namespace Day24.AttentionMeshOS.Models
{
    public sealed record SemanticPhysicsContext(
        GravityFieldNode Field,
        SemanticPhysicsState CurrentState,
        ParticipationMetrics ParticipationMetrics,
        float SemanticMass,
        float ResonanceScore,
        GravityFieldLifecycleState LifecycleState,
        SemanticPhysicsOptions Options,
        DateTimeOffset EvaluationAt
        );
}