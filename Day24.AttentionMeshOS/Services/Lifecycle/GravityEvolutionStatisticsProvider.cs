//gs
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class GravityEvolutionStatisticsProvider : IGravityEvolutionStatisticsProvider
    {
        private readonly GravityEvolutionMetricsAggregator _aggregator;

        public GravityEvolutionStatisticsProvider(GravityEvolutionMetricsAggregator aggregator)
        {
            _aggregator = aggregator;
        }

        public GravityEvolutionStatistics GetStatistics()
        {
            return _aggregator.GetStatistics();
        }
    }
}