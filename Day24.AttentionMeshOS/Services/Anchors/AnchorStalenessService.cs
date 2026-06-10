//gs
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;
using Microsoft.Extensions.Logging;
using System;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class AnchorStalenessService : IAnchorStalenessService

    {
        private readonly ILogger<AnchorStalenessService> _logger;
        
        public AnchorStalenessService(ILogger<AnchorStalenessService> logger)
        {
            _logger = logger;
        }

        public bool IsStale(AttentionBall attentionBall)
        {
            if (!attentionBall.IsAnchor)
                return false;

            var daysSinceLastAccessed = (DateTimeOffset.UtcNow - attentionBall.LastAccessedAt).TotalDays;

            var isStale = daysSinceLastAccessed > 30; // Consider stale if not accessed for more than 30 days

            if (isStale)
            {
                _logger.LogInformation(
                    "Anchor {id} is stale. Days since access : {days}",
                    attentionBall.Id,
                    Math.Round(daysSinceLastAccessed, 2));
            }

            return isStale;
        }
    }
}