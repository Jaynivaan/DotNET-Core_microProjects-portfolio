//gs
using Day24.AttentionMeshOS.Options;
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Linq;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class InputProcessingOrchestrator : IInputProcessingOrchestrator
    {
        private readonly IEnumerable<IInputProcessor> _processors;

        private readonly AttentionProcessingOptions _options;

        private readonly ILogger<InputProcessingOrchestrator> _logger;

        public InputProcessingOrchestrator(
            IEnumerable<IInputProcessor> processors,

            IOptions<AttentionProcessingOptions> options,

            ILogger<InputProcessingOrchestrator> logger
            )
        {
            _processors = processors;

            _options = options.Value;

            _logger = logger;
        }

        public async Task<ProcessorControl> ProcessAsync(
            InputProcessingContext context,
            CancellationToken cancellationToken = default)

        {
            if (!_options.Enabled)
            {
                _logger.LogWarning(
                    "Input Processing pipeline disabled by configuration.");

                _logger.LogInformation(
                    "  Skipped all input processors.");

                return ProcessorControl.Continue;
            }

            foreach ( var processor in _processors.OrderBy(
                          processor  => processor.ExecutionOrder))
            {

                try
                {
                    _logger.LogInformation(
                        "Executing Input Processor {ProcessorName}.",
                        processor.GetType().Name);

                    var result = await processor.ProcessAsync(
                    context,
                    cancellationToken);

                    if (result == ProcessorControl.ShortCircuit)
                    {
                        _logger.LogWarning(
                            "Input processing short-circuited by {ProcessorName}.",
                            processor.GetType().Name);

                        return ProcessorControl.ShortCircuit;
                    }
                }

                catch (Exception ex)
                {
                    if (processor.IsCritical)
                    {
                        _logger.LogError(
                            ex,
                            "Critical Input Processor {ProcessorName} failed.",
                            processor.GetType().Name);

                        context.ValidationResult.Errors.Add(
                        $"Critical input processor Failed: {processor.GetType().Name}"
                        );

                        return ProcessorControl.ShortCircuit;
                    }
                    _logger.LogWarning(
                        ex,
                        "Non-critical input processor {ProcessorName} failed. Continuing pipeline.",
                        processor.GetType().Name);

                }               
                
            }

            return ProcessorControl.Continue;
        }
    }
}