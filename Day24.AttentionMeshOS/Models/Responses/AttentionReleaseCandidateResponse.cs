//gs

using System;
using System.Collections.Generic;

namespace Day24.AttentionMeshOS.Models
{
    public sealed record AttentionReleaseCandidateResponse(
        Guid AttentionBallId,
        string CurrentAim,
        double AttentionWeight,
        double ReinforcementsPerHour,
        bool IsAnchor,
        bool IsReleaseCandidate,
        IReadOnlyList<string> Reasons
        );
}
