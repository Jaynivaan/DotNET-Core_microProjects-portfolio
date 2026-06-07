//gs
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Day24.AttentionMeshOS.Storage
{
    public sealed class InMemoryAttentionStore: IAttentionStore
    {
        private readonly List<AttentionBall> _attentionBalls = new();

        public void Save(AttentionBall attentionBall)
        {
            _attentionBalls.Add(attentionBall);
        }

        public void Update(AttentionBall attentionBall)
        {
            var index = _attentionBalls.FindIndex(
                ball => ball.Id == attentionBall.Id);

            if (index == -1)
                return;

            _attentionBalls[index] = attentionBall;
        }
        public IReadOnlyList<AttentionBall> GetAll()
        {
            return _attentionBalls;
        }
        public bool Delete(Guid attentionBallId)
        {
            var attentionBall = _attentionBalls.FirstOrDefault(ball => ball.Id == attentionBallId);

            if (attentionBall == null) return false;

            _attentionBalls.Remove(attentionBall);

            return true;
        }
    }
}