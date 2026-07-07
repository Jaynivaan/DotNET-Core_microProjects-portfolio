//gs
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;

using System.Diagnostics;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class SemanticQuantizationBenchmarkService : ISemanticQuantizationBenchmarkService
    {
        public SemanticQuantizationBenchmarkResult Benchmark(
            CandidateResolutionContext context,
            ICandidateResolver resolver)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            CandidateResolutionResult result =
                resolver.Resolve(context);

            stopwatch.Stop();


            return new SemanticQuantizationBenchmarkResult(
                resolver.Name,
                result.CandidateCount,
                stopwatch.ElapsedMilliseconds);
        }
    }
}