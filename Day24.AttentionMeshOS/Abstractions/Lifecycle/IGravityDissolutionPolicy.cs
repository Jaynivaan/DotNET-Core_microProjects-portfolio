//gs
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Abstractions
{
    public interface IGravityDissolutionPolicy
    {
        GravityDissolutionDecision Evaluate(
            GravityDissolutionCandidate candidate);
    }
}