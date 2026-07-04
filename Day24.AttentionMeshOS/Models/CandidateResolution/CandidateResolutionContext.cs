//gs

using System;

namespace Day24.AttentionMeshOS.Models
{
    public sealed record CandidateResolutionContext(
        sbyte[] IncomingSignature,
        sbyte[] PresenceMask,
        DateTimeOffset Timestamp,
        Guid? DynamicTagId = null,
        string? DynamicTagName = null
        );
}