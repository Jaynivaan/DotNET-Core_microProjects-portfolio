//gs
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;
using Microsoft.Extensions.Logging;
using System;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class AnchorDemotionService : IAnchorDemotionService
    {
        private readonly IAnchorStalenessService _stalenessService;
        private readonly ILogger<AnchorDemotionService> _logger;

        public AnchorDemotionService (
            IAnchorStalenessService stalenessService,
            ILogger<AnchorDemotionService> logger )
        {
            _stalenessService = stalenessService;
            _logger = logger;
        }

        public AttentionBall DemoteIfEligible(AttentionBall attentionBall)
        {
            if (!attentionBall.IsAnchor)
                return attentionBall;

            var isStale = _stalenessService.IsStale(attentionBall);

            var isWeak = attentionBall.AttentionWeight <= 0.50;

            var isRarelyReinforced = attentionBall.ReinforcementCount <= 2;

            var shouldDemote = isStale && isWeak && isRarelyReinforced;

            if (!shouldDemote )

                return attentionBall;

            _logger.LogInformation(
                "AttentionBall {id} demoted. Weight = {Weight}, ReinforcementCount = {Count}",
                attentionBall.Id,
                attentionBall.AttentionWeight,
                attentionBall.ReinforcementCount
                );
            return attentionBall with
            {
                IsAnchor = false,
                UpdatedAt = DateTimeOffset.UtcNow
            };
        }
    }
}