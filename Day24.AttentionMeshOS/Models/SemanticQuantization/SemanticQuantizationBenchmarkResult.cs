//gs

namespace Day24.AttentionMeshOS.Models
{
    public sealed record SemanticQuantizationBenchmarkResult(
        string ResolverName,
        int CandidateCount,
        long ElapsedMilliseconds
        );
}