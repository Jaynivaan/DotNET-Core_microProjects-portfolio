//gs

using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Options;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class TextNormalizationProcessor : IInputProcessor
    {
        private readonly TextNormalizationOptions _options;

        private readonly ILogger<TextNormalizationProcessor> _logger;

        public int ExecutionOrder => 2;

        public TextNormalizationProcessor(
            ILogger<TextNormalizationProcessor> logger,
            IOptions<TextNormalizationOptions> options
            )
        {
            _logger = logger;
            _options = options.Value;
        }

        public Task<ProcessorControl> ProcessAsync(
            InputProcessingContext context,
            CancellationToken cancellationToken = default )
        {
            if (!_options.Enabled)
            {
                _logger.LogInformation(
                    "Text Normalization Processor skipped by configuration for RawInput {RawInputId}.",
                    context.RawInput.Id);
                return Task.FromResult(
                    ProcessorControl.Continue);
            }
            var originalText = context.RawInput.Text;

            var normalizedText = originalText
                .Trim()
                .Replace("\r\n", " ")
                .Replace("\n", " ")
                .Replace("\t", " ");

            while (normalizedText.Contains("  "))
            {
                normalizedText = normalizedText.Replace("  ", " ");
            }

            context.NormalizedInput = new NormalizedInput(
                originalText,
                normalizedText
                );

            _logger.LogInformation(
                "Text Normalizer Processer executed for RawInput {RawInputId}. and the normalized text is [{NormalizedText}]",
                context.RawInput.Id, normalizedText );

            return Task.FromResult(
                ProcessorControl.Continue
                );
        }
    }
}