//gs

using System;
using System.Collections.Generic;

namespace Day24.AttentionMeshOS.Models
{
    public sealed class GravityFieldNode
    {
        public Guid FieldId { get; set; } = Guid.NewGuid();

        public bool IsAllocated { get; set; }

        public GravityFieldLifecycleState LifecycleState { get; set; } =
            GravityFieldLifecycleState.Dormant;

        public float SemanticMass { get; set; }

        public float AttentionEnergy { get; set; }

        public float StabilityScore { get; set; }

        public float FieldRadius { get; set; }

        public int[] GravityAccumulator { get; }

        public sbyte[] FieldSignature { get; }

        public List<Guid> ParticipatingDynamicTagIds { get; }

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        public DateTimeOffset LastEvolvedAt { get; set; } = DateTimeOffset.UtcNow;

        public GravityFieldNode(
            int centroidDimensions,
            int maxParticipatingTags)
        {
            GravityAccumulator = new int[centroidDimensions];

            FieldSignature = new sbyte[centroidDimensions];

            ParticipatingDynamicTagIds = new List<Guid>(maxParticipatingTags);
        }

    }
}