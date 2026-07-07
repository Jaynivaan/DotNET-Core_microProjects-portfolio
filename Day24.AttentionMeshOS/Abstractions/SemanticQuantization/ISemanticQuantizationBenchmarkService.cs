//gs
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Abstractions
{
    public interface ISemanticQuantizationBenchmarkService
    {
        SemanticQuantizationBenchmarkResult Benchmark(
            CandidateResolutionContext context,
            ICandidateResolver resolver);
    }
}