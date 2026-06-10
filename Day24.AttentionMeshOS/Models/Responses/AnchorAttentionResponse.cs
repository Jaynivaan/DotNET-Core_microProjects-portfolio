//gs
using System;

namespace Day24.AttentionMeshOS.Models
{
    public sealed record AnchorAttentionResponse(
        Guid Id,
        string CurrentAim,
        double AttentionWeight,
        DateTimeOffset LastAccessedAt,
        DateTimeOffset UpdatedAt
        );
}