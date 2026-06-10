//gs
using System.Collections.Generic;

namespace Day24.AttentionMeshOS.Models
{
    public sealed record PersistenceShot(
        string Text,
        AttentionBall ActiveBall,
        IReadOnlyList<Aspiration> Aspirations,
        IReadOnlyList<Tendency> Tendencies
        );
}