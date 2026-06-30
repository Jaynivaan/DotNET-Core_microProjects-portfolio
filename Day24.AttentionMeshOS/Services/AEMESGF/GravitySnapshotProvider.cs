//gs
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class GravitySnapshotProvider : IGravitySnapshotProvider
    {
        private readonly GravityRuntimeAggregator _aggregator;

        public GravitySnapshotProvider(GravityRuntimeAggregator aggregator)
        {
            _aggregator = aggregator;
        }

        public GravityRuntimeSnapshot GetSnapshot()
        {
            GravityRuntimeAggregate aggregate = _aggregator.CalculateMetrics();

            return new GravityRuntimeSnapshot(
                aggregate.Count,
                aggregate.TotalMass,
                aggregate.ActiveMemberships,
                aggregate.LargestFieldCount,
                aggregate.NewestFieldId,
                aggregate.StrongestFieldId,
                aggregate.AverageStability,
                aggregate.AverageRadius);
        }
    }
}