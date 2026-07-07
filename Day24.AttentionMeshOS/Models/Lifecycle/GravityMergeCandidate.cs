//gs
namespace Day24.AttentionMeshOS.Models
{
    public sealed record GravityMergeCandidate(
        Guid SourceFieldId,
        Guid TargetFieldId,
        double SimilarityScore,
        double MassRatio,
        double StabilityScore,
        string DecisionReason
        );
}