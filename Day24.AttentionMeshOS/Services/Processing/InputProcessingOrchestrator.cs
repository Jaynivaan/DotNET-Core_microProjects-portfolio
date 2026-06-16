//gs
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class InputProcessingOrchestrator : IInputProcessingOrchestrator
    {
        private readonly IEnumerable<IInputProcessor> _processors;

        private readonly ILogger<InputProcessingOrchestrator> _logger;

        public InputProcessingOrchestrator(
            IEnumerable<IInputProcessor> processors,
            ILogger<InputProcessingOrchestrator> logger
            )
        {
            _processors = processors;
            _logger = logger;
        }

        public async Task<ProcessorControl> ProcessAsync(
            InputProcessingContext context,
            CancellationToken cancellationToken = default)

        {
            foreach ( var processor in _processors.OrderBy(
                          processor  => processor.ExecutionOrder))
            {
                _logger.LogInformation(
                    "Executing Input Processor {ProcessorName}.",
                    processor.GetType().Name);

                var result = await processor.ProcessAsync(
                    context,
                    cancellationToken);
                
                if ( result == ProcessorControl.ShortCircuit)
                {
                    _logger.LogWarning(
                        "Input processing short-circuited by {ProcessorName}.",
                        processor.GetType().Name);
                    return ProcessorControl.ShortCircuit;
                }
            }

            return ProcessorControl.Continue;
        }
    }
}