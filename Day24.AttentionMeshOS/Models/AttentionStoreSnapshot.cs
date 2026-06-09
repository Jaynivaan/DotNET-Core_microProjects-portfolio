//gs
using System.Collections.Generic;

namespace Day24.AttentionMeshOS.Models
{
    public sealed record AttentionStoreSnapshot(
        List<AttentionBall> AttentionBalls,
        List<AttentionLink> AttentionLinks,
        List<ReinforcementEvent>ReinforcementEvents
        );
}