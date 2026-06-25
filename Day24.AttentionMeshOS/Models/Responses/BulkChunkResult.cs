//gs
namespace Day24.AttentionMeshOS.Models
{
    public sealed record BulkChunkResult(
        int ChunkIndex,
        bool Success,
        Guid? AttentionBallId,
        string? Error
        );
}