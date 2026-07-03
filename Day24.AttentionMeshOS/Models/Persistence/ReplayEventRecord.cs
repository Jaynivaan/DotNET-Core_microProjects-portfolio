//gs
using System;

namespace Day24.AttentionMeshOS.Models
{
    public sealed record ReplayEventRecord(
        Guid EventId,
        string EventType,
        DateTimeOffset OccurredAt,
        Guid EntityId,
        string PayloadHash,
        long SequenceNumber
        );
}