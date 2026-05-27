//gs

using Day24.AttentionMeshOS.Models;
using System.Collections.Generic;
namespace Day24.AttentionMeshOS.Abstractions
{
    public interface IAttentionStore
    {
        void Save(AttentionBall attentionBall);

        IReadOnlyList<AttentionBall> GetAll();
    }
}