//gs
using System.Collections.Generic;

namespace Day24.AttentionMeshOS.Models
{
    public sealed record CandidateResolutionResult(
        IReadOnlyList<CandidateFieldRef> Candidates,
        int CandidateCount,
        bool UsedFallback,
        string ResolverName
        );
}