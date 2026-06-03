//gs
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;
using Microsoft.Extensions.Logging;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class  AttentionPromotionService : IAttentionPromotionService
    {
        private readonly ILogger<AttentionPromotionService> _logger;

        public AttentionPromotionService(ILogger<AttentionPromotionService> logger)
        {
            _logger = logger;
        }

        public AttentionBall PromoteIfEligible(AttentionBall attentionBall)
        {
            if (attentionBall.IsAnchor)
            {
                _logger.LogInformation("Attention ball {Id} is already an anchor. No promotion needed.", attentionBall.Id);
                return attentionBall;
            }
         // = attentionBall.AttentionWeight >= 1.15 && attentionBall.ReinforcementCount >= 2;

            var shouldPromote = checkPromotionCriteria(attentionBall);       
            
            if (!shouldPromote)
            {
                _logger.LogInformation("Attention ball {Id} is not eligible for promotion.", attentionBall.Id);
                return attentionBall;
            }

            _logger.LogInformation(
                "AttentionBall {AttentionBallId} promoted to anchor!. Weight: {AttentionWeight}, Reinforcements: {ReinforcementCount}",
                attentionBall.Id,
                attentionBall.AttentionWeight,
                attentionBall.ReinforcementCount);

            // Implementation for promoting attention ball if eligible
            return attentionBall with
            {
                IsAnchor = true,
                UpdatedAt = DateTimeOffset.UtcNow
            };
        }
        private static bool checkPromotionCriteria(AttentionBall attentionBall)
        {
            return attentionBall.AttentionWeight >= 1.15 && attentionBall.ReinforcementCount >= 2;
        }
    }
}