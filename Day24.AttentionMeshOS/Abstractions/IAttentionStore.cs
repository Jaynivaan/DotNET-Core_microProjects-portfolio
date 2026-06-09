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

        void SaveReinforcementEvent(ReinforcementEvent reinforcementEvent);

        IReadOnlyList<AttentionBall> GetAll();

        IReadOnlyList<AttentionLink> GetLinks();

        IReadOnlyList<ReinforcementEvent> GetReinforcementEvents();

        bool Delete(Guid attentionBallId);

        
        
    }
}