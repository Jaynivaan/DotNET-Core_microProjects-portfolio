//gs

using System.Collections.Generic;

namespace Day24.AttentionMeshOS.Models
{
    public sealed record AttentionResponse(
        string CurrentAim,
        string ActiveProject,
        string MustNotForget,
        string NextMove,
        IReadOnlyList<string>Aspirations,
        IReadOnlyList<string>Tendencies,
        string PersistenceShot
        );
}