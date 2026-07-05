//gs
namespace Day24.AttentionMeshOS.Models
{
    public sealed record CandidateResolutionSnapshot(
        string ResolverName,
        int LastCandidateCount,
        double AverageCandidateCount,
        long FallbackCount,
        double CandidateReductionRatio
        );
}