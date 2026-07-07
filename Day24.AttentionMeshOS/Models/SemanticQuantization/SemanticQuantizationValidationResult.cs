//gs
namespace Day24.AttentionMeshOS.Models
{
    public sealed record SemanticQuantizationValidationResult(
        bool DeterministicQuantizationPassed,
        bool NeighborExpansionPassed,
        bool BucketMembershipInvariantPassed,
        bool DuplicateRegistrationDetected,
        bool OrphanedBucketDetected);
}