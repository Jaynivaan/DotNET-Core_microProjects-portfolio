//gs
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Options;
using Microsoft.Extensions.Options;
using System;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class AttentionDecayService : IAttentionDecayService
    {
        private readonly AttentionOptions _options;

        public AttentionDecayService(IOptions<AttentionOptions>options)
        {
            _options = options.Value;
        }

        public AttentionBall ApplyDecay(AttentionBall attentionBall)
        {
            var hoursSinceAccess = (DateTimeOffset.UtcNow - attentionBall.LastAccessedAt).TotalHours;

            var decayAmount = _options.DecayRate * hoursSinceAccess;

            if ( attentionBall.IsAnchor)
            {
                decayAmount *= _options.AnchorDecayMultiplier;
            }

            var minimumWeight = attentionBall.IsAnchor
                ? _options.AnchorMinimumWeight
                : _options.MinimumAttentionWeight;

            var newWeight = Math.Max(
                minimumWeight,
                attentionBall.AttentionWeight - decayAmount
                );

            return attentionBall with
            {
                AttentionWeight = Math.Round(newWeight, 3),
                UpdatedAt = DateTimeOffset.UtcNow
            };
        }
        
        public AttentionBall Boost (AttentionBall attentionBall)
        {
            var newWeight = attentionBall.AttentionWeight + _options.AttentionBoost;

            return attentionBall with
            {
                AttentionWeight = Math.Round(newWeight, 3),
                LastAccessedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };
        }
    }
}