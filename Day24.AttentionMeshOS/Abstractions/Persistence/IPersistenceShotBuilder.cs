//gs

using Day24.AttentionMeshOS.Models;
using System.Collections.Generic;
namespace Day24.AttentionMeshOS.Abstractions
{
    public interface IPersistenceShotBuilder
    {
        PersistenceShot Build(
            AttentionBall attentionBall,
            IReadOnlyList<Aspiration> aspirations,
            IReadOnlyList<Tendency> tendencies
            );
    }
}