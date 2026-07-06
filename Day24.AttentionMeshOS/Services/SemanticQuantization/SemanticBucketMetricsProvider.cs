//gs
using System.Linq;
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class SemanticBucketMetricsProvider : ISemanticBucketMetricsProvider
    {
        private readonly IBucketRegistry _bucketRegistry;

        public SemanticBucketMetricsProvider(IBucketRegistry bucketRegistry)
        {
            _bucketRegistry = bucketRegistry;
        }
        public SemanticBucketMetrics GetMetrics()
        {
            IReadOnlyList<SemanticBucketSnapshot> buckets =
                _bucketRegistry.GetSnapshots();

            int bucketCount = buckets.Count;

            if ( bucketCount == 0 )
            {
                return new SemanticBucketMetrics(
                    0,
                    0,
                    0d,
                    0,
                    0);
            }

            int totalEntries =
                buckets.Sum(bucket => bucket.EntryCount);

            int largestBucket = 
                buckets.Max(bucket => bucket.EntryCount);

            int smallestBucket =
                buckets.Min(bucket => bucket.EntryCount);

            double averageOccupancy =
                (double)totalEntries / bucketCount;

            return new SemanticBucketMetrics(
                bucketCount,
                totalEntries,
                averageOccupancy,
                largestBucket,
                smallestBucket);
        }
    }
}