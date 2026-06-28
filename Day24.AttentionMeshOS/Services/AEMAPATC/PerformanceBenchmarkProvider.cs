//gs

using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;
 
namespace Day24.AttentionMeshOS.Services
{
    public sealed class PerformanceBenchmarkProvider : IPerformanceBenchmarkProvider
    {
        public PerformanceBenchmark GetBenchmark()
        {
            return new PerformanceBenchmark(
                ProcessingLatencyMs: 0.0,
                SnapshotGenerationMs: 0.0,
                RegistryLookupMs: 0.0,
                MemoryAllocatedBytes: 0,
                ThroughputPerSecond: 0.0
                );
        }
    }
}