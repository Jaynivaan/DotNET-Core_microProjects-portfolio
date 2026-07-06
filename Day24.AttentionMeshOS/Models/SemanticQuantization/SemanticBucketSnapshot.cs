//gs
using System.Collections.Generic;

namespace Day24.AttentionMeshOS.Models
{
    public sealed record SemanticBucketSnapshot(
        SemanticBucketKey BucketKey,
        int EntryCount,
        IReadOnlyList<SemanticBucketEntry> Entries
        );
}