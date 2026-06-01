//gs
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Options;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System;

namespace Day24.AttentionMeshOS.Services
{
    public class AttentionReinforcementService: IAttentionReinforcementService
    {
        private readonly ILogger<AttentionReinforcementService> _logger;
        private readonly AttentionOptions _options;

        public AttentionReinforcementService (
            IOptions<AttentionOptions> options,
            ILogger<AttentionReinforcementService> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        public AttentionBall Reinforce(AttentionBall attentionBall)
        {
            var oldWeight = attentionBall.AttentionWeight;

            var newWeight =
                oldWeight + _options.AttentionBoost;

            _logger.LogInformation(
                "Reinforcement applied.Weight {OldWeight} => {newWeight}",
                oldWeight,
                newWeight);

            return attentionBall with
            {
                AttentionWeight = Math.Round(newWeight, 3),
                LastAccessedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };
        }
    }
}