//gs
using System;
using System.Collections.Generic;

namespace Day24.AttentionMeshOS.Models
{
    public sealed record CrystallizationContext(
        Guid CorrelationId,

        DateTimeOffset ReceivedAt,

        string SourceText,

        sbyte[] TernaryMask,

        IReadOnlyCollection<string> Keywords,

        IReadOnlyCollection<string> ExtractedTags
        );

}