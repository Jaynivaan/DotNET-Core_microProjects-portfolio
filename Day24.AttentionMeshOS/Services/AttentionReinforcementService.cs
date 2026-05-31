//gs
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Options;
using Microsoft.Extensions.Options;
using System;

namespace Day24.AttentionMeshOS.Services
{
    public class AttentionReinforcementService: IAttentionReinforcementService
    {
        private readonly AttentionOptions _options;

        public AttentionReinforcementService (IOptions<AttentionOptions> options)
        {
            _options = options.Value;
        }

        public AttentionBall Reinforce(AttentionBall attentionBall)
        {
            var NewWeight =
                attentionBall.AttentionWeight + _options.AttentionBoost;

            return attentionBall with
            {
                AttentionWeight = Math.Round(NewWeight, 3),
                LastAccessedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };
        }
    }
}