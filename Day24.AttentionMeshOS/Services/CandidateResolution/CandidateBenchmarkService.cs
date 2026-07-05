//gs
using System;
using System.Diagnostics;
using System.Linq;
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class CandidateBenchmarkService
    {
        private readonly IGravityRuntime _runtime;
        private readonly CandidateResolverSelector _selector;

        public CandidateBenchmarkService(
            IGravityRuntime runtime,
            CandidateResolverSelector selector)
        {
            _runtime = runtime;
            _selector = selector;
        }

        public CandidateBenchmarkResult RunBenchmark(
            CandidateResolutionContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            ICandidateResolver resolver = _selector.GetResolver();

            Stopwatch stopwatch = Stopwatch.StartNew();

            CandidateResolutionResult result =
                resolver.Resolve(context);

            stopwatch.Stop();

            int allocatedFieldCount =
                _runtime.Fields.Count(field => field.IsAllocated);
            double candidateReductionRatio =
                allocatedFieldCount == 0
                    ? 1d
                    : (double) result.CandidateCount / allocatedFieldCount;

            return new CandidateBenchmarkResult(
                result.ResolverName,
                allocatedFieldCount,
                result.CandidateCount,
                candidateReductionRatio,
                result.UsedFallback,
                stopwatch.ElapsedMilliseconds);

        }
    }
}