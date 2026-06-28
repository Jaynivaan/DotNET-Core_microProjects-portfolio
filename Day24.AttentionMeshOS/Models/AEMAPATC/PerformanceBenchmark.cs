//gs
namespace Day24.AttentionMeshOS.Models
{
    public readonly record struct PerformanceBenchmark(
        double ProcessingLatencyMs,
        double SnapshotGenerationMs,
        double RegistryLookupMs,
        long MemoryAllocatedBytes,
        double ThroughputPerSecond
        );
}