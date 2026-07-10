//gs
namespace Day24.AttentionMeshOS.Models
{
    public sealed record GravityEvolutionStatistics(

        long TotalEvolutionCycles,
        long TotalMergeCandidatesEvaluated,
        long TotalMergesExecuted,
        long TotalDissolutionCandidatesEvaluated,
        long TotalDissolutionsExecuted
        
        );
}