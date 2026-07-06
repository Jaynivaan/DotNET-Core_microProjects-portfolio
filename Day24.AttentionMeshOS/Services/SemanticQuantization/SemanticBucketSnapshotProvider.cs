//gs
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class SemanticBucketSnapshotProvider : ISemanticBucketSnapshotProvider
    {
        private readonly IBucketRegistry _bucketRegistry;
        private readonly ISemanticBucketMetricsProvider _metricsProvider;

        public SemanticBucketSnapshotProvider(
            IBucketRegistry bucketRegistry,
            ISemanticBucketMetricsProvider metricsProvider)
        {
            _bucketRegistry = bucketRegistry;
            _metricsProvider = metricsProvider;
        }
         
        public SemanticBucketRuntimeSnapshot GetSnapshot()
        {
            return new SemanticBucketRuntimeSnapshot(
                _metricsProvider.GetMetrics(),
                _bucketRegistry.GetSnapshots());
        }
    }
}