//gs
using System;

namespace Day24.AttentionMeshOS.Models
{
    public sealed record ReinforcementEvent(
        Guid AttentionBallId,
        double PreviousWeight,
        double NewWeight,
        DateTimeOffset ReinforcedAt
    );
}
