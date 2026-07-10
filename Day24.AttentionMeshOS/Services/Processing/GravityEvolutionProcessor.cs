//gs
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;
using Microsoft.Extensions.Logging;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class GravityEvolutionProcessor : IInputProcessor
    {

        private readonly ILogger<GravityEvolutionProcessor> _logger;
        private readonly IGravityRuntime _gravityRuntime;
        private readonly IGravityEvolutionEngine _gravityEvolutionEngine;
        private readonly GravityEvolutionMetricsAggregator _metricsAggregator;

        public int ExecutionOrder => 82;
        public bool IsCritical => true;

        public GravityEvolutionProcessor(
            ILogger<GravityEvolutionProcessor> logger,
            IGravityRuntime gravityRuntime,
            IGravityEvolutionEngine gravityEvolutionEngine,
            GravityEvolutionMetricsAggregator metricsAggregator
            )
        {
            _logger = logger;
            _gravityRuntime = gravityRuntime;
            _gravityEvolutionEngine = gravityEvolutionEngine;
            _metricsAggregator = metricsAggregator;
        }

        public Task<ProcessorControl>ProcessAsync(
            InputProcessingContext context,
            CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(context);

            GravityEvolutionContext evolutionContext =
                new(
                    Fields: _gravityRuntime.Fields,
                    EvaluationTime: context.RawInput.RecievedAt);

            GravityEvolutionResult result =
                _gravityEvolutionEngine.Evaluate(evolutionContext);

            _metricsAggregator.Record(
                result,
                evolutionContext.EvaluationTime);

            _logger.LogInformation(
                "Gravity evolution completed. MergeCandidates={MergeCandidates}, Merges={Merges}, DissolutionCandidates={DissolutionCandidates}, Dissolutions={Dissolutions}, EvolutionPerformed={EvolutionPerformed}",
                result.MergeCandidatesEvaluated,
                result.MergesExecuted,
                result.DissolutionCandidatesEvaluated,
                result.DissolutionsExecuted,
                result.EvolutionPerformed);

            return Task.FromResult(ProcessorControl.Continue);
        }
    }
}