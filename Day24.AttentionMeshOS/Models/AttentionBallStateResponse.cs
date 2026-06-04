//gs
using System;

namespace Day24.AttentionMeshOS.Models
{
    public sealed record AttentionBallStateResponse
        (
        Guid Id,
        string CurrentAim,
        double AttentionWeight,
        int ReinforcementCount,
        bool IsAnchor,
        bool IsStaleAnchor,
        DateTimeOffset LastAccessedAt,
        DateTimeOffset UpdatedAt
        );
}