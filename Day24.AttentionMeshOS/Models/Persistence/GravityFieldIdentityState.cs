//gs
using System;
using System.Collections.Generic;

namespace Day24.AttentionMeshOS.Models
{
    public sealed record GravityFieldIdentityState(
        Guid FieldId,
        string DisplayName,
        DateTimeOffset CreatedAt,
        string? SemanticFingerprint,
        string? StructuralHash,
        Guid? OriginEventId,
        IReadOnlyList<Guid>? ParentIds 

        );
}