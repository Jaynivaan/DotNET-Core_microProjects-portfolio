//gs
using System;
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Options;
using Microsoft.Extensions.Options;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class CandidateResolutionSnapshotProvider : ICandidateResolutionSnapshotProvider
    {
        private readonly CandidateResolutionMetricsProvider _metricsProvider;
        private readonly CandidateResolutionOptions _options;

        public CandidateResolutionSnapshotProvider(
            IOptions<CandidateResolutionOptions> options,
            CandidateResolutionMetricsProvider metricsProvider)
        {
            _options = options.Value;
            _metricsProvider = metricsProvider;
        }

        public CandidateResolutionSnapshot GetSnapshot()
        {
            CandidateResolutionMetrics metrics = _metricsProvider.Metrics;

            double reductionRatio = 0d;

            if ( _options.MaximumCandidateCount > 0 )
            {
                reductionRatio = 
                    metrics.AverageCandidateCount /
                    _options.MaximumCandidateCount;
            }

            return new CandidateResolutionSnapshot(
                metrics.LastResolverName,
                metrics.LastCandidateCount,
                metrics.AverageCandidateCount,
                metrics.FallbackCount,
                reductionRatio);
        }
    }
}
