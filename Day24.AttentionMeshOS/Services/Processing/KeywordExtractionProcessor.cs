//gs
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class KeywordExtractionProcessor : IInputProcessor
    {
        private readonly ILogger<KeywordExtractionProcessor> _logger;

        private readonly KeywordExtractionOptions _options;

        public int ExecutionOrder => 4;

        public bool IsCritical => false;

        public KeywordExtractionProcessor(
            ILogger<KeywordExtractionProcessor> logger,
            IOptions<KeywordExtractionOptions> options
            )
        {
            _logger = logger;
            _options = options.Value;
        }

        public Task<ProcessorControl>ProcessAsync(
            InputProcessingContext context,
            CancellationToken cancellationToken = default 
            )
        {
            if (!_options.Enabled)
            {
                _logger.LogInformation(
                    "Keywork Extraction Processor skipped by configuration for the RawInput {RawInputId}.",
                    context.RawInput.Id
                    );
                return Task.FromResult(ProcessorControl.Continue);
            }

            var inputText =
                context.NoiseReducedInput?.ReducedText
                ?? context.NormalizedInput?.NormalizedText
                ?? context.RawInput.Text;

            var keywords = inputText
                .Split(
                    ' ',
                    StringSplitOptions.RemoveEmptyEntries)
                .Select(word =>
                    _options.NormalizeKeywords 
                        ? word.ToLowerInvariant()
                        : word)
                .Where (word =>
                    word.Length >= _options.MinimumKeywordLength
                    && word.Length <= _options.MaximumKeywordLength)
                .Distinct()
                .ToList();
            context.KeywordExtractionResult =
                new KeywordExtractionResult(keywords);

            _logger.LogInformation(
                "keywords extraction processor extracted{keywordCount} keywords for RawInput {RawInputId}.",
                keywords.Count,
                context.RawInput.Id);

            return Task.FromResult(ProcessorControl.Continue);
        }
    }
}