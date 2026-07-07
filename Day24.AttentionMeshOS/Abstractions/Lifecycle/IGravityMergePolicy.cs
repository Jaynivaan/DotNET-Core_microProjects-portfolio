//gs
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Abstractions
{
    public interface IGravityMergePolicy
    {
        GravityMergeDecision Decide(
            GravityMergeCandidate candidate,
            GravityFieldNode source,
            GravityFieldNode target,
            DateTimeOffset evaluationTime);
    }
}