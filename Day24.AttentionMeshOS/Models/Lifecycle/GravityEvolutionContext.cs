//gs

using System.Collections.Generic;
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Models
{
    public sealed record GravityEvolutionContext(
        IReadOnlyList<GravityFieldNode> Fields,
        DateTimeOffset EvaluationTime
        );
}