//gs
namespace Day24.AttentionMeshOS.Models
{
    public sealed record BulkInputResponse(
        int TotalChunks,
        int SuccessfulChunks,
        int FailedChunks,
        IReadOnlyList<BulkChunkResult> Results
        );
}