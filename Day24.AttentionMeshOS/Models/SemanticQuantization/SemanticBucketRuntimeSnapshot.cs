//gs
using System.Collections.Generic;

namespace Day24.AttentionMeshOS.Models
{
    public sealed record SemanticBucketRuntimeSnapshot(
        SemanticBucketMetrics Metrics,
        IReadOnlyList<SemanticBucketSnapshot> Buckets);
}