//gs
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Abstractions
{
    public interface IGravityMergeEvaluator
    {
        GravityMergeCandidate Evaluate(
            GravityFieldNode source,
            GravityFieldNode target);
    }
}