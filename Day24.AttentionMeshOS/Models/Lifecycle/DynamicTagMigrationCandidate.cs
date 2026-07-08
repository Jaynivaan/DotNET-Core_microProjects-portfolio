//gs
namespace Day24.AttentionMeshOS.Models
{
    public sealed record  DynamicTagMigrationCandidate(
        Guid DynamicTagId,
        Guid SourceFieldId,
        Guid TargetFieldId,
        float SourceStability,
        float TargetStability,
        float SimilarityScore,
        bool SourceRetiring,
        string Reason);
}