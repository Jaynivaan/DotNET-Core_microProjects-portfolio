//gs
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;
using Microsoft.Extensions.Logging;
namespace Day24.AttentionMeshOS.Services
{

    public sealed class AttentionMeshBuilder : IAttentionMeshBuilder
    {
        private readonly ILogger<AttentionMeshBuilder> _logger;
        private readonly IAttentionStore _store;
        //private readonly ITextSimilarityService _similarityService;
        private readonly IAttentionResonanceService _resonanceService;
        private readonly IAttentionDecayService _decayService;
        private readonly IAttentionReinforcementService _reinforcementService;
        private readonly IAttentionPromotionService _promotionService;
        private readonly IAnchorDemotionService _demotionService;

        public AttentionMeshBuilder(
            ILogger<AttentionMeshBuilder> logger,
            IAttentionStore store,
            //ITextSimilarityService similarityService,
            IAttentionResonanceService resonanceService,
            IAttentionDecayService decayService,
            IAttentionReinforcementService reinforcementService,
            IAttentionPromotionService promotionService,
            IAnchorDemotionService demotionservice
            )
        {
            _logger = logger;
            _store = store;
            //_similarityService = similarityService;
            _resonanceService = resonanceService;
            _decayService = decayService;
            _reinforcementService = reinforcementService;
            _promotionService = promotionService;
            _demotionService = demotionservice;
        }
        public AttentionMesh Build(AttentionBall activeBall)
        {
            _logger.LogInformation(
                "Building mesh for AttentionBall {Id}", activeBall.Id);

            var relatedItems = _store.GetAll().ToList()
            .Where(ball => ball.Id != activeBall.Id)
            .Select(ball =>
            {
                var decayedBall = _decayService.ApplyDecay(ball);

                var resonance = _resonanceService.CalculateResonance(
                    activeBall,
                    decayedBall);

                _logger.LogInformation(
                    "Mesh resonance {sourceId} -> {TargetId} = {Score:F4}",
                    activeBall.Id,
                    decayedBall.Id,
                    resonance);

                var processedBall = resonance > 0
                    ? _reinforcementService.Reinforce(decayedBall)
                    : decayedBall;

                processedBall = _promotionService.PromoteIfEligible(processedBall);

                processedBall = _demotionService.DemoteIfEligible(processedBall);

                _store.Update(processedBall);

                return new
                {
                    Ball = processedBall,
                    Score = resonance * processedBall.AttentionWeight               
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
                "Semantic Resonance",
                item.Score,
                DateTimeOffset.UtcNow 
                ))
            .ToList();

            foreach (var link in links)
            {
                _store.SaveLink(link);

                _logger.LogInformation(
                    "AttentionLink persisted: {fromId} -> {ToId}, Strength => {Strength}",
                    link.FromId,
                    link.ToId,
                    link.Strength);
            }

            return new AttentionMesh(
            activeBall,
            relatedBalls,
            links
            );

        }
        
    }    

}