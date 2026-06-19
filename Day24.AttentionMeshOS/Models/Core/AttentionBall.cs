//gs
using System;

namespace Day24.AttentionMeshOS.Models
{
    public sealed record AttentionBall(
        Guid Id,
        Guid RawInputId,
        string CurrentAim,
        IReadOnlyList<string>Keywords,
        string ActiveProject,
        string MustNotForget,
        string NextMove,
        int PeristenceLevel,
        double AttentionWeight,
        int ReinforcementCount,
        bool IsAnchor ,
        DateTimeOffset UpdatedAt,
        DateTimeOffset LastAccessedAt
        );
}