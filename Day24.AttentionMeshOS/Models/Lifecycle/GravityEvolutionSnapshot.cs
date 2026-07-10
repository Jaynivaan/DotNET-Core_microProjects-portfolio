//gs
namespace Day24.AttentionMeshOS.Models
{
    public sealed record GravityEvolutionSnapshot(

        long TotalEvolutionCycles,
        long TotalMergeCandidatesEvaluated,
        long TotalMergesExecuted,
        long TotalDissolutionCandidatesEvaluated,
        long TotalDissolutionsExecuted,
        DateTimeOffset? LastEvaluationTime

        );
}