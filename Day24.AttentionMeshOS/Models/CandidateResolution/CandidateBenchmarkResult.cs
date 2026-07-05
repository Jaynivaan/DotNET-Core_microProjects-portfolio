//gs
namespace Day24.AttentionMeshOS.Models
{
    public sealed record CandidateBenchmarkResult(
        string ResolverName,
        int AllocatedFieldCount,
        int CandidateCount,
        double CandidateReductionRation,
        bool UsedFallback,
        long ElapsedMilliseconds
        );
}