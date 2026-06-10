//gs

using System.Collections.Generic;

namespace Day24.AttentionMeshOS.Models
{
    public sealed record AttentionStateResponse
        (
        int TotalAttentionBalls,
        IReadOnlyList<AttentionBallStateResponse>AttentionBalls
        );
}