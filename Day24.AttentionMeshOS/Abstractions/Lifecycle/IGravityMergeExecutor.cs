//gs
//gs
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Abstractions
{
    public interface IGravityMergeExecutor
    {
        bool Execute(
            GravityFieldNode source,
            GravityFieldNode target,
            DateTimeOffset mergedAt
            );
    }
}