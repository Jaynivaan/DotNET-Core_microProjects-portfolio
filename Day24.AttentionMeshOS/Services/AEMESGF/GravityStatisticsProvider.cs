//gs
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class GravityStatisticsProvider : IGravityStatisticsProvider
    {
        private readonly GravityRuntimeAggregator _aggregator;

        public GravityStatisticsProvider(GravityRuntimeAggregator aggregator)
        {
            _aggregator = aggregator;
        }

        public GravityRuntimeStatistics GetStatistics()
        {
            GravityRuntimeAggregate aggregate = _aggregator.CalculateMetrics();

            return new GravityRuntimeStatistics(
                FieldsCreated: aggregate.Count,
                FieldsMerged: 0,
                FieldsDissolved: 0,
                AverageFieldSize: aggregate.Count > 0
                    ? (double)aggregate.ActiveMemberships / aggregate.Count
                    : 0d,
                AverageSemanticMass: aggregate.Count > 0
                    ? aggregate.TotalMass / aggregate.Count
                    : 0d,
                MaximumObservedDensity: aggregate.LargestFieldCount,
                AverageMembershipsPerDynamicTag: 0d);
        }
    }
}