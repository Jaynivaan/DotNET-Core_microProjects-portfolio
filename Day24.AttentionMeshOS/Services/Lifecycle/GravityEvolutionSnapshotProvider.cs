//gs
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class GravityEvolutionSnapshotProvider : IGravityEvolutionSnapshotProvider
    {
        private readonly GravityEvolutionMetricsAggregator _aggregator;

        public GravityEvolutionSnapshotProvider(GravityEvolutionMetricsAggregator aggregator)
        {
            _aggregator = aggregator;
        }

        public GravityEvolutionSnapshot GetSnapshot()
        {
            return _aggregator.GetSnapshot();
        }
    }
}