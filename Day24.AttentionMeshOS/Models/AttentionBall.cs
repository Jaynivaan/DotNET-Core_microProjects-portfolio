//gs
using System;

namespace Day24.AttentionMeshOS.Models
{
    public sealed record AttentionBall(
        Guid Id,
        string CurrentAim,
        string ActiveProject,
        string MustNotForget,
        string NextMove,
        int PeristenceLevel,
        double AttentionWeight,
        bool IsAnchor ,
        DateTimeOffset UpdatedAt,
        DateTimeOffset LastAccessedAt
        );
}