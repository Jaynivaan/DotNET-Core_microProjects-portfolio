//gs
using System;

namespace Day24.AttentionMeshOS.Models
{
    public sealed record AttentionBallVelocity(

        Guid AttentionBallId,

        int ReinforcementCount,

        double ReinforcementsPerHour,

        DateTimeOffset WindowStart,

        DateTimeOffset WindowEnd
    );
}