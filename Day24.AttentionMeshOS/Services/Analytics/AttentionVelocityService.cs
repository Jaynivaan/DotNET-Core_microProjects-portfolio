//gs
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Options;
using Day24.AttentionMeshOS.Abstractions;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class AttentionVelocityService : IAttentionVelocityService
    {
        private readonly IAttentionStore _store;
        private readonly ILogger<AttentionVelocityService> _logger;
        private readonly AttentionVelocityOptions _options;
        public AttentionVelocityService(
            IAttentionStore store,
            ILogger <AttentionVelocityService>  logger,
            IOptions<AttentionVelocityOptions> options
            )
        {
            _store = store;
            _logger = logger;
            _options = options.Value;
        }

        public AttentionBallVelocity CalculateVelocity(
            
            Guid attentionBallId
            
            )
        {
            var window = TimeSpan.FromHours(
                _options.VelocityWindowHours
                );
            var windowEnd = DateTimeOffset.UtcNow;

            var windowStart = windowEnd.Subtract( window );

            var reinforcementCount = _store
                .GetReinforcementEvents()
                .Count(reinforcementEvent =>
                    reinforcementEvent.AttentionBallId == attentionBallId &&
                    reinforcementEvent.ReinforcedAt >= windowStart &&
                    reinforcementEvent.ReinforcedAt <= windowEnd);

            var hours = Math.Max(window.TotalHours, 1);

            var reinforcementsPerHour =
                Math.Round(reinforcementCount / hours, 4);

            _logger.LogInformation(
                "Calculated velocity for AttentionBall {id}: {velocity} reinforcements/hour over {hours} hours.",
                attentionBallId,
                reinforcementsPerHour,
                hours);

            return new AttentionBallVelocity(
                attentionBallId,
                reinforcementCount,
                reinforcementsPerHour,
                windowStart,
                windowEnd
                );

        }
    }
}