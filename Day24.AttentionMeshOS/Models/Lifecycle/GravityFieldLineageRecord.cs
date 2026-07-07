//gs
using System;
using System.Collections.Generic;

namespace Day24.AttentionMeshOS.Models
{
    public sealed record GravityFieldLineageRecord(
        Guid FieldId,
        IReadOnlyList<Guid> OriginFieldIds,
        IReadOnlyList<Guid> ParentFieldIds,
        Guid? MergedIntoFieldId,
        DateTimeOffset CreatedAt,
        DateTimeOffset? MergedAt,
        DateTimeOffset? DissolvedAt,
        string LineageReason);
}