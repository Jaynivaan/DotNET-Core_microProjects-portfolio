//gs
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Services
{

    public sealed class AttentionMeshBuilder : IAttentionMeshBuilder
    {
        private readonly IAttentionStore _store;

        public AttentionMeshBuilder(IAttentionStore store)
        {
            _store = store;
        }
        public AttentionMesh Build(AttentionBall activeBall)
        {
            var relatedBalls = _store.GetAll()
            .Where(ball => ball.Id != activeBall.Id)
            .Where(ball =>
                activeBall.CurrentAim.Contains(ball.CurrentAim, StringComparison.OrdinalIgnoreCase) ||
                ball.CurrentAim.Contains(activeBall.CurrentAim, StringComparison.OrdinalIgnoreCase))
            .Take(3)
            .ToList();

            var links = relatedBalls
            .Select(ball => new AttentionLink(
                activeBall.Id,
                ball.Id,
                "Related Attention Context",
                0.7))
            .ToList();

            return new AttentionMesh(
            activeBall,
            relatedBalls,
            links
            );

        }
        
    }    

}