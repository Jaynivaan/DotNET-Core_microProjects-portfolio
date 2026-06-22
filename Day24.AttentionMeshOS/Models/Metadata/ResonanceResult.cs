//gs

namespace Day24.AttentionMeshOS.Models
{
    public sealed record ResonanceResult(
        Guid SourceAttentionBallId,
        Guid TargetAttentionBallId,
        double ResonanceScore
    );
}