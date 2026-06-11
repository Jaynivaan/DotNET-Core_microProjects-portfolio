//gs
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Options;
using Day24.AttentionMeshOS.Abstractions;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class AttentionReleaseCandidateService: IAttentionReleaseCandidateService
    {
        private readonly IAttentionStore _store;
        private readonly IAttentionVelocityService _velocityService;
        private readonly AttentionReleaseOptions _options;
        private readonly ILogger<AttentionReleaseCandidateService> _logger;


        public AttentionReleaseCandidateService(
            IAttentionStore store,
            IAttentionVelocityService velocityService,
            IOptions<AttentionReleaseOptions> options,
            ILogger<AttentionReleaseCandidateService> logger 
            )
        {
            _store = store;
            _velocityService = velocityService;
            _options = options.Value;
            _logger = logger;
        }

        public IReadOnlyList<AttentionReleaseCandidateResponse> GetReleaseCandidates()
        {
            var candidates = _store.GetAll()
                .Select(CreateCandidateResponse)
                .ToList();

            _logger.LogInformation(
                "Release candidate evaluation completed. Evaluated {Count} AttentionBalls. ",
                candidates.Count);

            return candidates;
        }

        private AttentionReleaseCandidateResponse CreateCandidateResponse(
            AttentionBall ball)
        {
            var velocity = _velocityService.CalculateVelocity(ball.Id);

            var reasons = new List<string>();

            var hasLowWeight = ball.AttentionWeight <= _options.MaximumReleaseWeight;

            var hasLowVelocity = velocity.ReinforcementsPerHour <= _options.MaximumVelocityPerHour;

            var isAnchorProtected = ball.IsAnchor && !_options.AllowAnchorRelease;

            if (hasLowWeight) { reasons.Add("Low Attention weight"); }

            if (hasLowVelocity) { reasons.Add("Low Reinforcement Velocity"); }

            if (isAnchorProtected) { reasons.Add("Anchor Protected"); }

            var isReleaseCandidate =
                hasLowWeight &&
                hasLowVelocity &&
                !isAnchorProtected;

            return new AttentionReleaseCandidateResponse(
                ball.Id,
                ball.CurrentAim,
                ball.AttentionWeight,
                velocity.ReinforcementsPerHour,
                ball.IsAnchor,
                isReleaseCandidate,
                reasons
                );

        }
    }
}