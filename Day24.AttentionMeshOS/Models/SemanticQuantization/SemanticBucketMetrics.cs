//gs
namespace Day24.AttentionMeshOS.Models
{
    public sealed record SemanticBucketMetrics(
        int BucketCount,
        int TotalEntries,
        double AverageBucketOccupancy,
        int LargestBucketSize,
        int SmallestBucketSize);
}