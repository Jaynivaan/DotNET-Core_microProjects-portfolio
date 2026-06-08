//gs

using Day24.AttentionMeshOS.Models;
using System;
using System.Collections.Generic;
namespace Day24.AttentionMeshOS.Abstractions
{
    public interface IAttentionStore
    {
        void Save(AttentionBall attentionBall);

        void SaveLink(AttentionLink attentionLink);

        void Update(AttentionBall attentionBall);

        IReadOnlyList<AttentionBall> GetAll();

        IReadOnlyList<AttentionLink> GetLinks();

        bool Delete(Guid attentionBallId);

        
        
    }
}