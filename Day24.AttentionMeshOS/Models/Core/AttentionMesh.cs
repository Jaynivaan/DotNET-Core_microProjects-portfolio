//gs

using System;
using System.Collections.Generic;

namespace Day24.AttentionMeshOS.Models
{
    public sealed record AttentionMesh(
        AttentionBall ActiveBall,
        IReadOnlyList<AttentionBall> RelatedBalls,
        IReadOnlyList<AttentionLink> Links 
        );
}