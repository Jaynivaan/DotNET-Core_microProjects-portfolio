//gs
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class AnchorStateService : IAnchorStateService
    {
        private readonly ILogger<AnchorStateService> _logger;
        private readonly IAttentionStore _store;

        public AnchorStateService(
            IAttentionStore store,
            ILogger<AnchorStateService> logger
            )
        {
            _store = store;
            _logger = logger;
        }

        public IReadOnlyList<AnchorAttentionResponse> GetAnchors()
        {
            var anchors = _store.GetAll()
                .Where(ball => ball.IsAnchor)
                .Select(ball => new AnchorAttentionResponse
                (
                    ball.Id,
                    ball.CurrentAim,
                    ball.AttentionWeight,
                    ball.LastAccessedAt,
                    ball.UpdatedAt
                    
                    ))
                .ToList();

            _logger.LogInformation("Anchor state requested. Returning {Count } anchor AttentionBalls", anchors.Count);
            return anchors;
        }

    }
}