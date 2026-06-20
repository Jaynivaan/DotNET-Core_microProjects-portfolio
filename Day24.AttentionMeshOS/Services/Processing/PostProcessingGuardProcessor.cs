//gs
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Options;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class PostProcessingGuardProcessor : IInputProcessor
    {
        private readonly ILogger<PostProcessingGuardProcessor> _logger;
        private readonly PostProcessingGuardOptions _options;

        public int ExecutionOrder => 99;

        public bool IsCritical => true;

        public PostProcessingGuardProcessor(
            ILogger<PostProcessingGuardProcessor> logger,
            IOptions<PostProcessingGuardOptions> options
            )
        {
            _logger = logger;
            _options = options.Value;
        }

        public Task<ProcessorControl> ProcessAsync(
            InputProcessingContext context,
            CancellationToken cancellationToken = default
            )
        {
            if(!_options.Enabled)
            {
                return Task.FromResult(ProcessorControl.Continue);
            }

            var effectiveText = context.EffectiveText;

            if ( string.IsNullOrWhiteSpace( effectiveText ) )
            {
                context.ValidationResult.Errors.Add(
                    "Input became empty after processing."
                    );
                _logger.LogWarning(
                    "RawInput {RawInputId}  became empty after processing.",
                    context.RawInput.Id
                    );
                return Task.FromResult(ProcessorControl.ShortCircuit);

            }
            return Task.FromResult(ProcessorControl.Continue);
        }
    }
}