//gs
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class CandidateResolutionMetricsProvider
    {
        private readonly CandidateResolutionMetrics _metrics = new();

        public CandidateResolutionMetrics Metrics => _metrics;

        public void Record(
            string resolverName,
            int candidateCount,
            bool usedFallback)
        {
            _metrics.TotalResolutions++;

            _metrics.TotalCandidatesReturned += candidateCount;

            if ( usedFallback )
            {
                _metrics.FallbackCount++;
            }

            _metrics.LastResolverName = resolverName;
            _metrics.LastCandidateCount = candidateCount;

            _metrics.AverageCandidateCount =
                (double)_metrics.TotalCandidatesReturned /
                _metrics.TotalResolutions;
        }
    }
}