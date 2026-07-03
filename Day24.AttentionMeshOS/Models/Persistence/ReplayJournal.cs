//gs
using System;
using System.Collections.Generic;

namespace Day24.AttentionMeshOS.Models
{
    public sealed record ReplayJournal(
        int ReplayVersion,
        DateTimeOffset CreatedAt,
        IReadOnlyList<ReplayEventRecord> Events
        );
}