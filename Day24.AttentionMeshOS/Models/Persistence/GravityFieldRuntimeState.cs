//gs
using System;
using System.Collections.Generic;

namespace Day24.AttentionMeshOS.Models
{
    public sealed record GravityFieldRuntimeState(
        Guid FieldId,
        bool IsAllocated,
        GravityFieldLifecycleState LifecycleState,
        float SemanticMass,
        int[] GravityAccumulator,
        sbyte[] FieldSignature,
        IReadOnlyList<DynamicTagParticipationState> Participants,
        DateTimeOffset CreatedAt,
        DateTimeOffset LastEvolvedAt,
        SemanticPhysicsStateRecord Physics
        );
}