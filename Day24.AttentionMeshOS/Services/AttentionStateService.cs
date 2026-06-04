//gs
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;
using Microsoft.Extensions.Logging;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class AttentionStateService : IAttentionStateService
    {
        private readonly ILogger<AttentionStateService> _logger;
        private readonly IAttentionStore _store;
        private readonly IAnchorStalenessService _stalenessService;

        public AttentionStateService(
            IAttentionStore store,
            ILogger<AttentionStateService> logger,
            IAnchorStalenessService stalenessService)
        {
            _logger = logger;
            _store = store;
            _stalenessService = stalenessService;
        }
        public AttentionStateResponse GetState()
        {
            var balls = _store.GetAll()
                .Select(ball=> new AttentionBallStateResponse(
                    ball.Id,
                    ball.CurrentAim,
                    ball.AttentionWeight,
                    ball.ReinforcementCount,
                    ball.IsAnchor,
                    _stalenessService.IsStale(ball),
                    ball.LastAccessedAt,
                    ball.UpdatedAt))
                .ToList();

            _logger.LogInformation(
                "Attention State requested .Returning {Coung} AttentionBalls.",
                balls.Count);

            return new AttentionStateResponse(
                balls.Count,
                balls);
        }
    }

}