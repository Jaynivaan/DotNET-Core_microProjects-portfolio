//gs
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;
using Microsoft.Extensions.Logging;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class CrystallizationProcessor: IInputProcessor
    {
        private readonly ICrystallizationEngine _crystallizationEngine;
        private readonly ILogger<CrystallizationProcessor> _logger;

        public int ExecutionOrder => 80;

        public bool IsCritical => true;

        public CrystallizationProcessor(
            ICrystallizationEngine crystallizationEngine,
            ILogger<CrystallizationProcessor> logger )
        {
            _crystallizationEngine = crystallizationEngine;
            _logger = logger;
        }

        public Task<ProcessorControl> ProcessAsync (
            InputProcessingContext context,
            CancellationToken cancellationToken = default)
        {
            if (context.VectorPreparationResult is null)
            {
                return Task.FromResult(ProcessorControl.Continue);
            }

            var ternaryMask = BuildTernaryMask(
                context.VectorPreparationResult.TextForEmbedding);

            var crystallizationContext = new CrystallizationContext(
                CorrelationId: context.RawInput.Id,
                ReceivedAt: context.RawInput.RecievedAt,
                TernaryMask: ternaryMask,
                Keywords: context.VectorPreparationResult.Keywords,
                ExtractedTags: context.VectorPreparationResult.Tags);

            var Result = _crystallizationEngine.Process(crystallizationContext);

            context.CrystallizationResult = Result;

            if (Result.WasCrystallized)
            {
                _logger.LogInformation(
                    "AEM-APATC dynamic tag crystallized : {TagName}",
                    Result.CrystallizedTagName);
            }
            return Task.FromResult(ProcessorControl.Continue);

        }

        private static sbyte[] BuildTernaryMask (string text)
        {
            var mask = new sbyte[text.Length];

            for (int i = 0; i < text.Length; i++)
            {
                int bucket = text[i] % 3;

                mask[i] = bucket switch
                {
                    0 => (sbyte)-1,
                    1 => (sbyte)0,
                    _ => (sbyte)1
                };
            }
            return mask;

        }
    }
}