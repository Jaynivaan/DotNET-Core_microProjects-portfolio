//gs
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Services
{

    public sealed class AttentionMeshBuilder : IAttentionMeshBuilder
    {
        private readonly IAttentionStore _store;
        private readonly ITextSimilarityService _similarityService;

        public AttentionMeshBuilder(
            IAttentionStore store,
            ITextSimilarityService similarityService
            )
        {
            _store = store;
            _similarityService = similarityService;
        }
        public AttentionMesh Build(AttentionBall activeBall)
        {
            var relatedItems = _store.GetAll()
            .Where(ball => ball.Id != activeBall.Id)
            .Select(ball => new
            {
                Ball = ball,
                Score = _similarityService.CalculateSimilarity(
                    activeBall.CurrentAim,
                    ball.CurrentAim)
            })
            .Where(item => item.Score > 0)
            .OrderByDescending(item => item.Score)
            .Take(3)
            .ToList();

            var relatedBalls = relatedItems
                .Select(item => item.Ball)
                .ToList();


            var links = relatedItems
            .Select(item  => new AttentionLink(
                activeBall.Id,
                item.Ball.Id,
                "Keyword Overlap Similarity",
                item.Score))
            .ToList();

            return new AttentionMesh(
            activeBall,
            relatedBalls,
            links
            );

        }
        
    }    

}