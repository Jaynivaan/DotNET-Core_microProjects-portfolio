//gs
namespace Day24.AttentionMeshOS.Models
{
    public sealed record GravityEvolutionResult(
        int MergeCandidatesEvaluated,
        int MergesExecuted,
        int DissolutionCandidatesEvaluated,
        int DissolutionsExecuted,
        bool EvolutionPerformed);

}