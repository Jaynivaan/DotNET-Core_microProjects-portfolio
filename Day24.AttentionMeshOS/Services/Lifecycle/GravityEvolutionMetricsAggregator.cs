//gs
using Day24.AttentionMeshOS.Models;


namespace Day24.AttentionMeshOS.Services
{
    public sealed class GravityEvolutionMetricsAggregator
    {
        private readonly object _syncLock = new();

        private long _totalEvolutionCycles;
        private long _totalMergeCandidatesEvaluated;
        private long _totalMergesExecuted;
        private long _totalDissolutionCandidatesEvaluated;
        private long _totalDissolutionsExecuted;
        private DateTimeOffset? _lastEvaluationTime;

        public void Record(
            GravityEvolutionResult result,
            DateTimeOffset evaluationTime
            )
        {
            ArgumentNullException.ThrowIfNull(result);

            lock (_syncLock)
            {
                _totalEvolutionCycles++;
                _totalMergeCandidatesEvaluated += result.MergeCandidatesEvaluated;
                _totalMergesExecuted += result.MergesExecuted;
                _totalDissolutionCandidatesEvaluated += result.DissolutionCandidatesEvaluated;
                _totalDissolutionsExecuted += result.DissolutionsExecuted;
                _lastEvaluationTime = evaluationTime;

            }
        }

        public GravityEvolutionSnapshot GetSnapshot()
        {
            lock (_syncLock)
            {
                return new GravityEvolutionSnapshot(
                    _totalEvolutionCycles,
                    _totalMergeCandidatesEvaluated,
                    _totalMergesExecuted,
                    _totalDissolutionCandidatesEvaluated,
                    _totalDissolutionsExecuted,
                    _lastEvaluationTime
                    );
            }
        }

        public GravityEvolutionStatistics GetStatistics()
        {
            lock ( _syncLock)
            {
                return new GravityEvolutionStatistics(
                    _totalEvolutionCycles,
                    _totalMergeCandidatesEvaluated,
                    _totalMergesExecuted,
                    _totalDissolutionCandidatesEvaluated,
                    _totalDissolutionsExecuted
                    );
            }
        }
    }
}
