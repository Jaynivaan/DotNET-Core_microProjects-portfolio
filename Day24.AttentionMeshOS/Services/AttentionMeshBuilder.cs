//gs
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Services
{

    public sealed class AttentionMeshBuilder : IAttentionMeshBuilder
    {
        private readonly IAttentionStore _store;
        private readonly ITextSimilarityService _similarityService;
        private readonly IAttentionDecayService _decayService;
        private readonly IAttentionReinforcementService _reinforcementService;

        public AttentionMeshBuilder(
            IAttentionStore store,
            ITextSimilarityService similarityService,
            IAttentionDecayService decayService,
            IAttentionReinforcementService reinforcementService 
            )
        {
            _store = store;
            _similarityService = similarityService;
            _decayService = decayService;
            _reinforcementService = reinforcementService;
        }
        public AttentionMesh Build(AttentionBall activeBall)
        {
            var relatedItems = _store.GetAll().ToList()
            .Where(ball => ball.Id != activeBall.Id)
            .Select(ball =>
            {
                var decayedBall = _decayService.ApplyDecay(ball);

                var similarity = _similarityService.CalculateSimilarity(
                    activeBall.CurrentAim, decayedBall.CurrentAim);

                var processedBall = similarity > 0
                    ? _reinforcementService.Reinforce(decayedBall)
                    : decayedBall;
                _store.Update(processedBall);

                return new
                {
                    Ball = processedBall,
                    Score = similarity * processedBall.AttentionWeight               
                };

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